using SQLite;
using System.Collections.Generic;

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

        // Used by the SQLite ORM
        public Lobby()
        {
            Drawers = new Dictionary<string, User>();
        }

        public Lobby(string GroupName)
        {
            Drawers = new Dictionary<string, User>();
            Canvas = new Canvas();
            CanvasId = Canvas.Id;
            this.GroupName = GroupName;

            Lobbies.Add(GroupName, this);
        }

        public void AddUser(User User)
        {
            Drawers.Add(User.SessionId, User);
        }
    }
}