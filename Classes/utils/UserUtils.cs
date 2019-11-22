using Microsoft.AspNetCore.SignalR;
using SQLite;
using System.Threading.Tasks;

namespace csharp_08.Utils
{
    /// <summary>
    /// UserUtils contains websockets tasks that handle user changes
    /// </summary>
    public static class UserUtils
    {
        /// <summary>
        /// UpdateUserPermission updates the user global permissions: objects edition and deletion
        /// Send to all user in same lobby the updated permission for this user.
        /// </summary>
        /// <param name="Context">Caller connection context</param>
        /// <param name="Clients">All connected clients</param>
        /// <param name="permission">New permission. Should be a number to parse</param>
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