using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace csharp_08
{

    public enum ShapeCode : byte { Line = 1, Pencil, Circle, Text, Polygon, Point };


    public class ConnectionHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Debug.WriteLine("New connection ...");
            // Connect to local DB
            SQLiteConnection db = new SQLiteConnection("Data Source=database.db;Version=3;");
            db.Open();

            string id = Context.ConnectionId;
            string username = Context.GetHttpContext().Request.Query["username"];
            string group = Context.GetHttpContext().Request.Query["lobby"];

            await base.OnConnectedAsync();

            if (!Lobby.Lobbies.TryGetValue(group, out var lobby))
            {
                lobby = new Lobby(group);

                SQLiteCommand lobbySQL = new SQLiteCommand("INSERT INTO Lobbies VALUES (@name, @canvasId)", db);
                lobbySQL.Parameters.Add(new SQLiteParameter("@name", DbType.String));
                lobbySQL.Parameters.Add(new SQLiteParameter("@canvasId", DbType.Int32));
                lobbySQL.Parameters["@name"].Value = lobby.GroupName;
                lobbySQL.Parameters["@canvasId"].Value = lobby.Canvas.Id;

                SQLiteCommand canvasSQL = new SQLiteCommand("INSERT INTO Canvas VALUES (@id, @data)", db);
                canvasSQL.Parameters.Add(new SQLiteParameter("@id", DbType.Int32));
                canvasSQL.Parameters.Add(new SQLiteParameter("@data", DbType.String));
                canvasSQL.Parameters["@id"].Value = lobby.Canvas.Id;
                canvasSQL.Parameters["@data"].Value = "{ }";

                lobbySQL.ExecuteNonQuery();
                canvasSQL.ExecuteNonQuery();
            }

            // Create user
            User user = new User(id, username, group);
            await Groups.AddToGroupAsync(user.ConnectionId, group);
            lobby.AddUser(user);

            // Insert user into the table "Users"
            SQLiteCommand userSQL = new SQLiteCommand("INSERT INTO Users VALUES (@id, @username, @lobby, @permissions)", db);
            userSQL.Parameters.Add(new SQLiteParameter("@id", DbType.String));
            userSQL.Parameters.Add(new SQLiteParameter("@username", DbType.String));
            userSQL.Parameters.Add(new SQLiteParameter("@lobby", DbType.String));
            userSQL.Parameters.Add(new SQLiteParameter("@permissions", DbType.Int32));
            userSQL.Parameters["@id"].Value = user.ConnectionId;
            userSQL.Parameters["@username"].Value = user.Username;
            userSQL.Parameters["@lobby"].Value = user.Lobby;
            userSQL.Parameters["@permissions"].Value = user.OverridePermissions;

            int affected = userSQL.ExecuteNonQuery();
            Debug.WriteLine(affected);

            // Close connection to the database
            db.Close();

            await Clients.Caller.SendAsync("ID", id);
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
            Lobby lobby = Lobby.Lobbies[User.Users[id].Lobby];
            User.Users.Remove(id);
            lobby.Drawers.Remove(id);

            await base.OnDisconnectedAsync(exception);
            await Clients.Group(lobby.GroupName).SendAsync("drawers", JsonConvert.SerializeObject(lobby.Drawers));
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
            Debug.WriteLine("New shape received");
            string id = Context.ConnectionId;
            Lobby lobby = Lobby.Lobbies[User.Users[id].Lobby];

            Shape shape = GetShapeFromJSON(byte.Parse(shapeType), newShape);
            shape.Owner = User.Users[id];
            lobby.Canvas.Shapes.Add(shape.ID, shape);

            await Clients.Group(lobby.GroupName).SendAsync("newShape", shapeType, shape);
        }

        public async Task UpdateShape(string shapeType, string newShape)
        {
            string id = Context.ConnectionId;
            User user = User.Users[id];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            Shape updatedShape = GetShapeFromJSON(byte.Parse(shapeType), newShape);
            Shape oldShape = lobby.Canvas.Shapes[updatedShape.ID];

            if (((oldShape.Owner.OverridePermissions & 1) != (oldShape.OverrideUserPolicy & 1)) || oldShape.Owner == user)
            {
                oldShape.UpdateWithNewShape(updatedShape);

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
            uint shapeID = uint.Parse(shapeIDstring);
            User user = User.Users[id];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            var deletedShape = lobby.Canvas.Shapes[shapeID];

            if ((deletedShape.Owner.OverridePermissions >> 1 != deletedShape.OverrideUserPolicy >> 1) || deletedShape.Owner == user) 
            {
                lobby.Canvas.Shapes.Remove(deletedShape.ID);
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
            uint shapeID = uint.Parse(shapeIDstring);
            byte newPermission = byte.Parse(permission);
            User user = User.Users[id];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            var Shape = lobby.Canvas.Shapes[shapeID];

            if (Shape.Owner == user)
            {
                Shape.OverrideUserPolicy = newPermission;
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
            User user = User.Users[id];
            Lobby lobby = Lobby.Lobbies[user.Lobby];
            byte newPermission = byte.Parse(permission);
            user.OverridePermissions = newPermission;
            await Clients.Group(lobby.GroupName).SendAsync("newUserPermission", id, newPermission);
        }
    }
}
