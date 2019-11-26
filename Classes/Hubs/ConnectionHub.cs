using csharp_08.Utils;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp_08
{
    /// <summary>
    /// SignalR Hub class that handles websocket connections.
    /// This class is instanciated by the signalR library and added as hub in startup.cs
    /// </summary>
    public class ConnectionHub : Hub
    {
        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            Debug.WriteLine("New connection ...");
            // Connect to local DB
            SQLiteConnection db = new SQLiteConnection("database.db");

            string id = Context.ConnectionId;
            string username = Context.GetHttpContext().Request.Query["username"];
            string group = Context.GetHttpContext().Request.Query["lobby"];
            string sessionId = Context.GetHttpContext().Request.Query["sessionId"];

            await base.OnConnectedAsync();

            if (!Lobby.Lobbies.TryGetValue(group, out var lobby))
            {
                lobby = new Lobby(group);

                db.Insert(lobby);
                db.Insert(lobby.Canvas);
            }

            User user;

            // Create user
            if (sessionId != null)
            {
                user = User.Users[sessionId];
                user.ConnectionId = id;
                User.ConnectionIdSessionIdTranslationTable.Add(user.ConnectionId, user.SessionId);
                db.Update(user);
            }
            else
            {
                user = new User(id, username, group);
                db.Insert(user);
            }
            await Groups.AddToGroupAsync(user.ConnectionId, group);
            lobby.AddUser(user);

            await Clients.Caller.SendAsync("ID", user.SessionId);
            await Clients.Group(group).SendAsync("drawers", JsonConvert.SerializeObject(lobby.Drawers));

            foreach (Shape shape in lobby.Canvas.Shapes.Values)
            {
                await Clients.Caller.SendAsync("newShape", shape.GetShapeCode(), shape);
            }
            await Clients.Caller.SendAsync("newBgColor", lobby.Canvas.BackgroundColor);
        }

        /// <summary>
        /// Called when a connection with the hub is terminated.
        /// </summary>
        /// <param name="exception"></param>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Debug.WriteLine("Disconnecting ...");
            string id = Context.ConnectionId;

            if (User.ConnectionIdSessionIdTranslationTable.TryGetValue(id, out string sessionId))
            {
                Lobby lobby = Lobby.Lobbies[User.Users[sessionId].Lobby];
                lobby.Drawers.Remove(sessionId);
                await base.OnDisconnectedAsync(exception);
                await Clients.Group(lobby.GroupName).SendAsync("drawers", JsonConvert.SerializeObject(lobby.Drawers));
                // executed in the background - we don't want to wait for it
                var ignoredTask = Task.Delay(1000 * 60 * 5).ContinueWith(async t =>
                {
                    SQLiteConnection db = new SQLiteConnection("database.db");
                    if (!lobby.Drawers.ContainsKey(sessionId)) // enable object edition and deletion
                    {
                        Debug.WriteLine("removing user policies...");
                        foreach (KeyValuePair<uint, Shape> entry in lobby.Canvas.Shapes)
                        {
                            if (entry.Value.Owner == User.Users[sessionId])
                            {
                                entry.Value.OverrideUserPolicy = 0b11; // can edit and can delete
                                await Clients.Group(lobby.GroupName).SendAsync("newShapePermission", entry.Value.ID, entry.Value.OverrideUserPolicy);
                            }
                        }
                        db.Update(lobby.Canvas);
                    }
                    if (lobby.Drawers.Count == 0) // delete lobby after 10min without anyone in the lobby
                    {
                        Debug.WriteLine("removing lobby");
                        Lobby.Lobbies.Remove(User.Users[sessionId].Lobby);
                        db.Delete(lobby.Canvas);
                        db.Delete(lobby);
                    }
                });
            }
            else
            {
                throw new Exception("Unknown connection ID");
            }
        }

        /// <summary>
        /// Called when a user wants to add a shape.
        /// </summary>
        /// <param name="shapeType">Shape type: </param>
        /// <param name="newShape">JSON shape definition</param>
        public async Task AddShape(string shapeType, string newShape)
        {
            await ShapeUtils.AddShape(Context, Clients, shapeType, newShape);
        }

        /// <summary>
        /// Called when a user wants to update a shape.
        /// </summary>
        /// <param name="shapeType">Shape type: </param>
        /// <param name="updatedShape">JSON shape definition. Should contain ID to retrieve the shape to update.</param>
        public async Task UpdateShape(string shapeType, string updatedShape)
        {
            await ShapeUtils.UpdateShape(Context, Clients, shapeType, updatedShape);
        }

        /// <summary>
        /// Called when a user wants to delete a shape.
        /// </summary>
        /// <param name="shapeIDString">Shape id</param>
        public async Task DeleteShape(string shapeIDString)
        {
            await ShapeUtils.DeleteShape(Context, Clients, shapeIDString);
        }

        /// <summary>
        /// Called when a user wants to update a shape permissions (edition and deletion).
        /// </summary>
        /// <param name="shapeIDString">Shape id</param>
        /// <param name="permission">
        ///     New permission: should be 2-bit string like '3' -> 0b11.
        ///     First bit allows edition. Second bit allows deletion.
        /// </param>
        public async Task UpdateShapePermission(string shapeIDString, string permission)
        {
            await ShapeUtils.UpdateShapePermission(Context, Clients, shapeIDString, permission); ;
        }

        /// <summary>
        /// Called when a user wants to update his permissions.
        /// </summary>
        /// <param name="permission">
        ///     New permission: should be 2-bit string like '3' -> 0b11.
        ///     First bit allows edition. Second bit allows deletion.
        /// </param>
        /// <returns></returns>
        public async Task UpdateUserPermission(string permission)
        {
            await UserUtils.UpdateUserPermission(Context, Clients, permission);
        }

        /// <summary>
        /// Called when a user wants to update the canvas background color.
        /// </summary>
        /// <param name="color">hex color</param>
        public async Task UpdateBackgroundColor(string color)
        {
            await ShapeUtils.UpdateBackgroundColor(Context, Clients, color);
        }

        /// <summary>
        /// Called when a user wants to download the canvas as SVG.
        /// </summary>
        public async Task GetSVG()
        {
            await ShapeUtils.GetSVG(Context, Clients);
        }
    }
}