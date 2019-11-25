using System.Collections.Generic;
using SQLite;

namespace csharp_08
{
    [Table("Lobbies")]
    public class Lobby
    {
        [PrimaryKey, Unique]
        public string GroupName { get; set; }

        public uint CanvasId { get; set; }

        [Ignore]
        public static Dictionary<string, Lobby> Lobbies { get; } = new Dictionary<string, Lobby>();

        [Ignore]
        public Dictionary<string, User> Drawers { get; private set; }

        [Ignore]
        public Canvas Canvas { get; set; }

        /// <summary>
        /// Default contructor. Used by the SQLite ORM
        /// </summary>
        public Lobby()
        {
            Drawers = new Dictionary<string, User>();
        }

        /// <summary>
        /// Create a new lobbie named GroupName.
        /// </summary>
        /// <param name="GroupName">Lobby's name</param>
        public Lobby(string GroupName)
        {
            Drawers = new Dictionary<string, User>();
            Canvas = new Canvas();
            CanvasId = Canvas.Id;
            this.GroupName = GroupName;

            Lobbies.Add(GroupName, this);
        }

        /// <summary>
        /// Add a new user to the lobby
        /// </summary>
        /// <param name="User">User object describing the new user</param>
        public void AddUser(User User)
        {
            Drawers.Add(User.SessionId, User);
        }
    }
}