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

        public static Dictionary<string, User> Users { get; private set; } = new Dictionary<string, User>();

        public User(string ConnectionId, string Username, string Lobby)
        {
            this.ConnectionId = ConnectionId;
            this.Username = Username;
            this.Lobby = Lobby;

            Users.Add(ConnectionId, this);
        }
    }
}
