using System;
using System.Collections.Generic;
using SQLite;

namespace csharp_08
{
    /// <summary>
    /// This class is used to represent a user. It stores his SignalR connection ID
    /// as well as his username.
    /// </summary>

    // Create a new table in the database to store Users
    [Table("Users")]
    public class User
    {
        // Use sessionId (generate by the C# program) as primary key
        [PrimaryKey, Unique]
        public string SessionId { get; set; }

        [Ignore] // Ignore because connectinId is not kept over reboot
        public string ConnectionId { get; set; }

        public string Username { get; set; }
        public string Lobby { get; set; }
        public byte OverridePermissions { get; set; } // 0 to 3: Bit0: Update Permission Bit1: Deletion Permission

        // A translation table between connection IDs and session IDs. Used to determines from where the request come.
        [Ignore]
        public static Dictionary<string, string> ConnectionIdSessionIdTranslationTable { get; private set; } = new Dictionary<string, string>();

        [Ignore]
        public static Dictionary<string, User> Users { get; private set; } = new Dictionary<string, User>();

        /// <summary>
        /// Default User constructor, used by the SQLite ORM
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// User constructor
        /// </summary>
        /// <param name="ConnectionId">User's connection ID (given by SignalR)</param>
        /// <param name="Username">User's username (gien by the frontend)</param>
        /// <param name="Lobby">User's lobby</param>
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