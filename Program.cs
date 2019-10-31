using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SQLite;
using System.Diagnostics;

namespace csharp_08
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Entering Main ...");

            Debug.WriteLine("Creating Database (if not exists) ...");

            SQLiteConnection db = new SQLiteConnection("database.db");

            db.CreateTable<User>();
            db.CreateTable<Lobby>();
            db.CreateTable<Canvas>();

            RetrieveDataFromDatabase(db);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static void RetrieveDataFromDatabase(SQLiteConnection db)
        {
            TableQuery<Lobby> lobbies = db.Table<Lobby>();
            uint LastCanvasID = 0;
            foreach (Lobby lobby in lobbies)
            {
                Lobby.Lobbies.Add(lobby.GroupName, lobby);
                lobby.Canvas = db.Table<Canvas>().Where(canvas => canvas.Id == lobby.CanvasId).First();

                if (lobby.Canvas.SerializedShapes != null)
                    lobby.Canvas.Deserialize();

                LastCanvasID = (lobby.CanvasId > LastCanvasID) ? lobby.CanvasId : LastCanvasID;
            }
            Canvas.SetIdStartPoint(LastCanvasID + 1);

            TableQuery<User> users = db.Table<User>();
            foreach (User user in users)
            {
                User.Users.Add(user.SessionId, user);
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}