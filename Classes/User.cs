using SQLite;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, Unique]
        public string SessionId { get; set; }

        [Ignore] // Ignore because connectinId is not kept over reboot
        public string ConnectionId { get; set; }

        public string Username { get; set; }
        public string Lobby { get; set; }
        public byte OverridePermissions { get; set; } // 0 to 3: Bit0: Update Permission Bit1: Deletion Permission

        [Ignore]
        public static Dictionary<string, string> ConnectionIdSessionIdTranslationTable { get; private set; } = new Dictionary<string, string>();

        [Ignore]
        public static Dictionary<string, User> Users { get; private set; } = new Dictionary<string, User>();

        public User()
        {
        }

        public User(string ConnectionId, string Username, string Lobby)
        {
            this.SessionId = Guid.NewGuid().ToString();
            this.ConnectionId = ConnectionId;
            this.Username = Username;
            this.Lobby = Lobby;
            this.OverridePermissions = 0;

            ConnectionIdSessionIdTranslationTable.Add(ConnectionId, SessionId);
            Users.Add(SessionId, this);
        }
    }
}