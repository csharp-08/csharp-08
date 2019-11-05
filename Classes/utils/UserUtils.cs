using Microsoft.AspNetCore.SignalR;
using SQLite;
using System.Threading.Tasks;

namespace csharp_08.Utils
{
    public static class UserUtils
    {
        public static async Task UpdateUserPermission(HubCallerContext Context, IHubCallerClients Clients, string permission)
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