using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp_08
{
    public class ConnectionHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            string id = Context.ConnectionId;
            string username = Context.GetHttpContext().Request.Query["username"];
            string group = Context.GetHttpContext().Request.Query["lobby"];

            await base.OnConnectedAsync();

            /* Lobby lobby;
            if (!Lobby.Lobbies.ContainsKey(group))
            {
                lobby = new Lobby(group);
            }
            else
            {
                lobby = Lobby.Lobbies[group];
            } */
            if (!Lobby.Lobbies.TryGetValue(group, out var lobby))
            {
                lobby = new Lobby(group);
            }

            User user = new User(id, username, group);
            await Groups.AddToGroupAsync(user.ConnectionId, group);
            lobby.AddUser(user);

            await Clients.Caller.SendAsync("ID", id);
            await Clients.Group(group).SendAsync("drawers", JsonConvert.SerializeObject(lobby.Drawers));

            foreach (Shape shape in lobby.Canvas.Shapes.Values)
            {
                await Clients.Caller.SendAsync("newShape", shape.GetType().Name, shape);
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string id = Context.ConnectionId;
            Lobby lobby = Lobby.Lobbies[User.Users[id].Lobby];
            User.Users.Remove(id);
            lobby.Drawers.Remove(id);

            await base.OnDisconnectedAsync(exception);
            await Clients.Group(lobby.GroupName).SendAsync("drawers", JsonConvert.SerializeObject(lobby.Drawers));
        }

        private static Shape GetShapeFromJSON(string shapeType, string newShape)
        {
            switch (shapeType)
            {
                case "Line":
                    return JsonConvert.DeserializeObject<Line>(newShape);
                case "Pencil":
                    return JsonConvert.DeserializeObject<Pencil>(newShape);
                case "Circle":
                    return JsonConvert.DeserializeObject<Circle>(newShape);
                case "Text":
                    return JsonConvert.DeserializeObject<Text>(newShape);
                default:
                    Debug.WriteLine("not done yet");
                    return null;
            }
        }

        public async Task AddShape(string shapeType, string newShape)
        {
            string id = Context.ConnectionId;
            Lobby lobby = Lobby.Lobbies[User.Users[id].Lobby];

            Shape shape = GetShapeFromJSON(shapeType, newShape);
            shape.Owner = User.Users[id];
            lobby.Canvas.Shapes.Add(shape.ID, shape);

            await Clients.Group(lobby.GroupName).SendAsync("newShape", shapeType, shape);
        }

        public async Task UpdateShape(string shapeType, string newShape)
        {
            string id = Context.ConnectionId;
            Lobby lobby = Lobby.Lobbies[User.Users[id].Lobby];

            Shape updatedshape = GetShapeFromJSON(shapeType, newShape);
            Shape oldShape = lobby.Canvas.Shapes[updatedshape.ID];
            oldShape.UpdateWithNewShape(updatedshape);

            await Clients.Group(lobby.GroupName).SendAsync("updateShape", shapeType, oldShape);
        }
    }
}
