using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace csharp_08
{
    public class Lobby
    {
        public static Dictionary<string, Lobby> Lobbies { get; private set; } = new Dictionary<string, Lobby>();
        public Dictionary<string, User> Drawers { get; private set; }
        public string GroupName;
        public Canvas Canvas;

        public Lobby(string GroupName)
        {
            Drawers = new Dictionary<string, User>();
            Canvas = new Canvas();
            this.GroupName = GroupName;

            Lobbies.Add(GroupName, this);
        }

        public void AddUser(User User)
        {
            Drawers.Add(User.ConnectionId, User);
        }
    }
}
