using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp_08
{
    public class Lobby : Hub
    {
        public static List<User> drawers = new List<User>();
        public static Canvas canvas = new Canvas();

        public override async Task OnConnectedAsync()
        {
            string id = Context.ConnectionId;
            string username = Context.GetHttpContext().Request.Query["username"];

            drawers.Add(new User(id, username));
            Debug.WriteLine(drawers.Count);

            Debug.WriteLine($"{username} is connected!");
            await base.OnConnectedAsync();

            await Clients.All.SendAsync("drawers", JsonConvert.SerializeObject(drawers));
        }
    }
}
