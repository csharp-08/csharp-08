using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp_08
{
    public class Lobby : Hub
    {
        public static Dictionary<string, User> drawers = new Dictionary<string, User>();
        public static Canvas canvas = new Canvas();

        public override async Task OnConnectedAsync()
        {
            string id = Context.ConnectionId;
            string username = Context.GetHttpContext().Request.Query["username"];

            drawers[id] = new User(id, username);

            Debug.WriteLine(drawers.Count);

            Debug.WriteLine($"{username} is connected!");
            await base.OnConnectedAsync();

            await Clients.All.SendAsync("drawers", JsonConvert.SerializeObject(drawers));
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string id = Context.ConnectionId;
            await base.OnDisconnectedAsync(exception);
            drawers.Remove(id);
            await Clients.All.SendAsync("drawers", JsonConvert.SerializeObject(drawers));
        }
    }
}
