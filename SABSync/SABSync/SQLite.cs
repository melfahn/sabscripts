using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace SABSync
{
    class SQLite
    {
        //SABSync.db
        //History table

        public void SetupDatabase()
        {
            //Create new DB
            CreateDatabase();

            //Create tables
            string shows =
                @"CREATE TABLE shows(
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    show_name TEXT,
                    tvdb_id INTEGER,
                    tvdb_name TEXT,
                    quality INTEGER,
	                ignore_season int,
	                aliases TEXT,
	                air_day TEXT,
	                air_time TEXT,
	                run_time INTEGER,
	                status TEXT,
	                poster_url TEXT,
	                banner_url TEXT,
	                imdb_id TEXT,
	                genre TEXT,
	                overview TEXT
	                )";
            
            string episodes =
                @"CREATE TABLE episodes(
	               id INTEGER PRIMARY KEY AUTOINCREMENT,
	               show_id INTEGER,
	               season_number INTEGER,
	               episode_number INTEGER,
	               episode_name TEXT,
	               air_date TEXT,
	               tvdb_id INTEGER,
	               overview TEXT,
	               FOREIGN KEY(show_id) REFERENCES shows(id)
	               )";

            string histories =
                @"CREATE TABLE histories(
	               id INTEGER PRIMARY KEY AUTOINCREMENT,
	               show_id INTEGER,
	               episode_id INTEGER,
	               feed_title TEXT,
	               quality INTERGER,
	               proper INTEGER,
	               provider TEXT,
	               date TEXT,
	               FOREIGN KEY(show_id) REFERENCES shows(id),
	               FOREIGN KEY(episode_id) REFERENCES episodes(id)
	               )";

            string info =
                @"CREATE TABLE info(
	                id INTEGER PRIMARY KEY AUTOINCREMENT,
                    last_tvdb INTEGER
                    )";

            string providers =
                @"CREATE TABLE providers(
                   id INTEGER PRIMARY KEY AUTOINCREMENT,
                   name TEXT,
                   url TEXT
                   )";

            ExecuteNonQuery(shows);
            ExecuteNonQuery(episodes);
            ExecuteNonQuery(histories);
            ExecuteNonQuery(info);
            ExecuteNonQuery(providers);
        }

        private void CreateDatabase()
        {
            //Create SABSync.db
            SQLiteConnection.CreateFile("SABSync.db");
        }

        public void ExecuteNonQuery(string command)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=SABSync.db"); //Create new connection to SABSync.db
            con.Open(); //Open the connection
            SQLiteCommand cmd = new SQLiteCommand(con); //Create a new command using the connection to SABSync.db
            cmd.CommandText = command;
            cmd.ExecuteNonQuery(); //Execute command without capturing output
            con.Close(); //Close the connection
        }
    }
}