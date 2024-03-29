﻿using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;

namespace csharp_08.Utils
{
    public enum ShapeCode : byte { Line = 1, Pencil, Circle, Text, Polygon };

    public static class ShapeUtils
    {
        /// <summary>
        /// Transform an input json (from the client) to a Shape object
        /// </summary>
        /// <param name="shapeType">Shape code (see ShapeCode enum)</param>
        /// <param name="newShape">JSON formatted string</param>
        /// <returns></returns>
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

                default:
                    Debug.WriteLine("not done yet");
                    return null;
            }
        }

        /// <summary>
        /// Add a new shape to a Canvas
        /// </summary>
        /// <param name="Context">SignalR context</param>
        /// <param name="Clients">SignalR clients</param>
        /// <param name="shapeType">Shape code (see ShapeCode enum)</param>
        /// <param name="newShape">JSON formatted string</param>
        /// <returns></returns>
        public static async Task AddShape(HubCallerContext Context, IHubCallerClients Clients, string shapeType, string newShape)
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

        /// <summary>
        /// Update a shape in a Canvas
        /// </summary>
        /// <param name="Context">SignalR context</param>
        /// <param name="Clients">SignalR clients</param>
        /// <param name="shapeType">Shape code (see ShapeCode enum)</param>
        /// <param name="newShape">JSON formatted string</param>
        /// <returns></returns>
        public static async Task UpdateShape(HubCallerContext Context, IHubCallerClients Clients, string shapeType, string newShape)
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
                db.Update(lobby.Canvas);

                await Clients.Group(lobby.GroupName).SendAsync("updateShape", shapeType, oldShape);
            }
            else
            {
                await Clients.Caller.SendAsync("updateShape", null, null);
            }
        }

        /// <summary>
        /// Delete a shape from a Canvas
        /// </summary>
        /// <param name="Context">SignalR context</param>
        /// <param name="Clients">SignalR clients</param>
        /// <param name="shapeIDString">Shape ID (sent by the client)</param>
        /// <returns></returns>
        public static async Task DeleteShape(HubCallerContext Context, IHubCallerClients Clients, string shapeIDString)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            uint shapeID = uint.Parse(shapeIDString);
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

        /// <summary>
        /// Update shape's permissions (allow or deny shape updating or deletion)
        /// </summary>
        /// <param name="Context">SignalR context</param>
        /// <param name="Clients">SignalR clients</param>
        /// <param name="shapeIDString">Shape ID (sent by the client)</param>
        /// <param name="permission">New permissions (0 to 3: 1st bit is Update and 2nd is Delete)</param>
        /// <returns></returns>
        public static async Task UpdateShapePermission(HubCallerContext Context, IHubCallerClients Clients, string shapeIDString, string permission)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            uint shapeID = uint.Parse(shapeIDString);
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

        /// <summary>
        /// Convert a Color object into a string. Used to generate the SVG.
        /// </summary>
        /// <param name="color">Color to be converted</param>
        /// <returns>An hexadecimal string #RRGGBB</returns>
        public static string ColorToHex(Color color)
        {
            return String.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        /// <summary>
        /// Update Canvas' background color
        /// </summary>
        /// <param name="Context">SignalR context</param>
        /// <param name="Clients">SignalR clients</param>
        /// <param name="color">New background color</param>
        /// <returns></returns>
        public static async Task UpdateBackgroundColor(HubCallerContext Context, IHubCallerClients Clients, string color)
        {
            // Connect to local DB
            SQLiteConnection db = new SQLiteConnection("database.db");

            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            Lobby lobby = Lobby.Lobbies[User.Users[sessionId].Lobby];
            Canvas canvas = lobby.Canvas;
            canvas.BackgroundColor = color;
            db.Update(canvas);

            await Clients.Group(lobby.GroupName).SendAsync("newBgColor", color);
        }

        /// <summary>
        /// Get SVG from a Canvas
        /// </summary>
        /// <param name="Context">SignalR context</param>
        /// <param name="Clients">SignalR clients</param>
        /// <returns></returns>
        public static async Task GetSVG(HubCallerContext Context, IHubCallerClients Clients)
        {
            string id = Context.ConnectionId;
            string sessionId = User.ConnectionIdSessionIdTranslationTable[id];

            User user = User.Users[sessionId];
            Lobby lobby = Lobby.Lobbies[user.Lobby];

            await Clients.Caller.SendAsync("SVG", lobby.Canvas.ToSVG());
        }
    }
}