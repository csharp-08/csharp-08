using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SQLite;
using System.Diagnostics;

namespace csharp_08
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Debug.WriteLine("Entering Main ...");

            Debug.WriteLine("Creating Database (if not exists) ...");

            // Create file if not exists
            if (!File.Exists("database.db"))
            {
                SQLiteConnection.CreateFile("database.db");
                SQLiteConnection db = new SQLiteConnection("Data Source=database.db;Version=3;");
                db.Open();

                SQLiteCommand lobbys = new SQLiteCommand("CREATE TABLE Canvas (id INT PRIMARY KEY NOT NULL, data TEXT)", db);
                SQLiteCommand canvas = new SQLiteCommand("CREATE TABLE Lobbies (name VARCHAR(255) PRIMARY KEY NOT NULL, canvasId INT)", db);
                SQLiteCommand users = new SQLiteCommand("CREATE TABLE Users (id VARCHAR(255) PRIMARY KEY NOT NULL, username VARCHAR(255), lobby VARCHAR(255), overridePermisions INT)", db);

                canvas.ExecuteNonQuery();
                lobbys.ExecuteNonQuery();
                users.ExecuteNonQuery();

                db.Close();
            }
            Debug.WriteLine("Database created !");


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
