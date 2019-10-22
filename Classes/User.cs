using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_08
{
    public class User
    {
        public string ConnectionId { get; private set; }
        public string Username { get; private set; }
        public string Lobby { get; private set; }
        public byte OverridePermissions { get; set; } // 0 to 3: Bit0: Update Permission Bit1: Deletion Permission

        public static Dictionary<string, User> Users { get; private set; } = new Dictionary<string, User>();

        public User(string ConnectionId, string Username, string Lobby)
        {
            this.ConnectionId = ConnectionId;
            this.Username = Username;
            this.Lobby = Lobby;
            this.OverridePermissions = 0;

            Users.Add(ConnectionId, this);
        }
    }
}
