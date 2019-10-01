using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp_08
{
    public class Lobby : Hub
    {
        public List<User> Drawers { get; private set; }
        public Canvas Canvas { get; set; }

        public Lobby()
        {
            this.Drawers = new List<User>();
            this.Canvas = new Canvas();
        }

        public override async Task OnConnectedAsync()
        {
            string id = Context.ConnectionId;
            string username = Context.GetHttpContext().Request.Query["username"];
            this.Drawers.Add(new User(id, username));

            Debug.WriteLine($"{username} is connected!");
            await base.OnConnectedAsync();
        }

    }
}
