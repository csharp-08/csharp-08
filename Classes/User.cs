using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_08
{
    public class User
    {
        public string ID { get; private set; }
        public string Username { get; private set; }

        public User(string ID, string Username)
        {
            this.ID = ID;
            this.Username = Username;
        }
    }
}
