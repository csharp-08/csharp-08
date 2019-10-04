using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            Lobby lobby;
            if (! Lobby.Lobbies.ContainsKey(group)){
                lobby = new Lobby(group);
            } else
            {
                lobby = Lobby.Lobbies[group];
            }

            User user = new User(id, username, group);
            await Groups.AddToGroupAsync(user.ConnectionId, group);
            lobby.AddUser(user);

            await Clients.Caller.SendAsync("ID", id);
            await Clients.Group(group).SendAsync("drawers", JsonConvert.SerializeObject(lobby.Drawers));

            foreach (Shape shape in lobby.Canvas.ShapeList)
            {
                await Clients.Caller.SendAsync("newShape", "Line", shape);
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

        public async Task AddShape(string shapeType, string newShape)
        {
            string id = Context.ConnectionId;
            Lobby lobby = Lobby.Lobbies[User.Users[id].Lobby];

            Debug.WriteLine(shapeType);
            Shape shape = null;

            switch (shapeType)
            {
                case "Line":
                    Debug.WriteLine(newShape);
                    shape = JsonConvert.DeserializeObject<Line>(newShape);
                    break;
                default:
                    Debug.WriteLine("not done yet");
                    return;
            }
            lobby.Canvas.Add(shape);
            // Debug.WriteLine(canvas);


            // JObject shape = JObject.Parse(shapeInput);
            // Debug.WriteLine(shape.GetValue("toolName"));

            await Clients.Group(lobby.GroupName).SendAsync("newShape", shapeType, shape);
        }
    }
}
