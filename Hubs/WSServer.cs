using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Threading.Tasks;

namespace csharp08.Hubs
{
    public class WSServer : Hub
    {
        public static string[] PlayerNames { get; private set; }

        public override async Task OnConnectedAsync()
        {
            Debug.WriteLine(Context.ConnectionId);
            await base.OnConnectedAsync();
            //return Clients.All.SendAsync("ReceiveMessage");
        }

    }
}
