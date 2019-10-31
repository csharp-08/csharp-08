using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp_08
{
    public enum ShapeCode : byte { Line = 1, Pencil, Circle, Text, Polygon, Point };

    public class ConnectionHub : Hub
    {
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
        }

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
            }
            else
            {
                throw new Exception("Unknown connection ID");
            }
        }

        private static Shape GetShapeFromJSON(byte shapeType, string newShape)
        {
            switch (shapeType)
            {
                case (byte)ShapeCode.Line:
                    return JsonConvert.DeserializeObject<Line>(newShape, new ColorJsonConverter());

                case (byte)ShapeCode.Pencil:
                    return JsonConvert.DeserializeObject<Pencil>(newShape, new ColorJsonConverter());

                case (byte)ShapeCode.Circle:
                    return JsonConvert.DeserializeObject<Circle>(newShape, new ColorJsonConverter());

                case (byte)ShapeCode.Text:
                    return JsonConvert.DeserializeObject<Text>(newShape, new ColorJsonConverter());

                case (byte)ShapeCode.Polygon:
                    return JsonConvert.DeserializeObject<Polygon>(newShape, new ColorJsonConverter());

                case (byte)ShapeCode.Point:
                    return JsonConvert.DeserializeObject<Point>(newShape, new ColorJsonConverter());

                default:
                    Debug.WriteLine("not done yet");
                    return null;
            }
        }

        public async Task AddShape(string shapeType, string newShape)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            Lobby lobby = Lobby.Lobbies[User.Users[sessionId].Lobby];

            Shape shape = GetShapeFromJSON(byte.Parse(shapeType), newShape);
            shape.Owner = User.Users[sessionId];
            lobby.Canvas.Shapes.Add(shape.ID, shape);

            lobby.Canvas.Serialize();
            SQLiteConnection db = new SQLiteConnection("database.db");
            db.Update(lobby.Canvas);

            await Clients.Group(lobby.GroupName).SendAsync("newShape", shapeType, shape);
        }

        public async Task UpdateShape(string shapeType, string newShape)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            User user = User.Users[sessionId];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            Shape updatedShape = GetShapeFromJSON(byte.Parse(shapeType), newShape);
            Shape oldShape = lobby.Canvas.Shapes[updatedShape.ID];

            if (((oldShape.Owner.OverridePermissions & 1) != (oldShape.OverrideUserPolicy & 1)) || oldShape.Owner == user)
            {
                oldShape.UpdateWithNewShape(updatedShape);

                lobby.Canvas.Serialize();
                SQLiteConnection db = new SQLiteConnection("database.db");
                db.Insert(lobby.Canvas);

                await Clients.Group(lobby.GroupName).SendAsync("updateShape", shapeType, oldShape);
            }
            else
            {
                await Clients.Caller.SendAsync("updateShape", null, null);
            }
        }

        public async Task DeleteShape(string shapeIDstring)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            uint shapeID = uint.Parse(shapeIDstring);
            User user = User.Users[sessionId];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            var deletedShape = lobby.Canvas.Shapes[shapeID];

            if ((deletedShape.Owner.OverridePermissions >> 1 != deletedShape.OverrideUserPolicy >> 1) || deletedShape.Owner == user)
            {
                lobby.Canvas.Shapes.Remove(deletedShape.ID);

                lobby.Canvas.Serialize();
                SQLiteConnection db = new SQLiteConnection("database.db");
                db.Update(lobby.Canvas);

                await Clients.Group(lobby.GroupName).SendAsync("deleteShape", deletedShape.ID);
            }
            else
            {
                await Clients.Caller.SendAsync("deleteShape", null);
            }
        }

        public async Task UpdateShapePermission(string shapeIDstring, string permission)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            uint shapeID = uint.Parse(shapeIDstring);
            byte newPermission = byte.Parse(permission);
            User user = User.Users[sessionId];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            var Shape = lobby.Canvas.Shapes[shapeID];

            if (Shape.Owner == user)
            {
                Shape.OverrideUserPolicy = newPermission;

                SQLiteConnection db = new SQLiteConnection("database.db");
                db.Update(lobby.Canvas);

                await Clients.Group(lobby.GroupName).SendAsync("newShapePermission", Shape.ID, Shape.OverrideUserPolicy);
            }
            else
            {
                await Clients.Caller.SendAsync("newShapePermission", null, null);
            }
        }

        public async Task UpdateUserPermission(string permission)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            User user = User.Users[sessionId];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            byte newPermission = byte.Parse(permission);
            user.OverridePermissions = newPermission;

            SQLiteConnection db = new SQLiteConnection("database.db");
            db.Update(user);

            await Clients.Group(lobby.GroupName).SendAsync("newUserPermission", sessionId, newPermission);
        }
    }
}