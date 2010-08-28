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
            //Create new DB if it doesn't exist
            //Create table if Database was created

            CreateDatabase();
            string historyTableCommand = "CREATE TABLE history(recordnumber INTEGER PRIMARY KEY, showname TEXT, showid NUMERIC, season NUMERIC, episode NUMERIC, episodename TEXT, feedtitle TEXT, quality NUMERIC, proper NUMERIC, provider TEXT, date TEXT)";
            ExecuteNonQuery(historyTableCommand);
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