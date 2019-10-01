using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace csharp_08.Hubs
{
    public class CanvasHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}