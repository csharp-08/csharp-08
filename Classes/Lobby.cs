using System.Collections.Generic;

namespace csharp_08
{
    public class Lobby
    {
        public static Dictionary<string, Lobby> Lobbies { get; } = new Dictionary<string, Lobby>();
        public Dictionary<string, User> Drawers { get; private set; }
        public string GroupName;
        public Canvas Canvas { get; private set; }

        public Lobby(string GroupName)
        {
            Drawers = new Dictionary<string, User>();
            Canvas = new Canvas();
            this.GroupName = GroupName;

            Lobbies.Add(GroupName, this);
        }

        public void AddUser(User User)
        {
            Drawers.Add(User.SessionId, User);
        }
    }
}