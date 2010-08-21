using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using MonoTorrent.Common;


namespace Sab2Torrent
{
    class Program
    {
        private static string PATH_SEP = Path.DirectorySeparatorChar.ToString(); //Store the Path Separator Character in an easy to use variable

        private static DirectoryInfo _logDir;
        private static string _logFile;
        private static bool _uploadToServer;
        private static bool _addToUTorrent;
        private static bool _torrentPrivate;
        private static DirectoryInfo _torrentDir;
        private static List<string> _torrentTrackers;
        private static FileInfo _uTorrentExe;

        static void Main(string[] args)
        {
            LoadConfig(); //Load the Config

            //Use args[0] for Path & args[2] for Name
            string downloadPath = args[0]; //Get moviePath from first CMD Line argument

            if (downloadPath.EndsWith(PATH_SEP))
            {
                downloadPath = downloadPath.Substring(0, downloadPath.Length - 2);
            }

            // if passed with just one arg (not just sabnzbd passing), make movienamesab last directory entry
            string downloadName = (args.Length == 1) ? LastEntry(downloadPath, PATH_SEP) : args[2]; //Get downloadName from third CMD Line argument

            string torrentFile = String.Format("{0}{1}{2}.torrent", _torrentDir, Path.DirectorySeparatorChar,
                                               downloadName);

            CreateTorrent(downloadPath, torrentFile, downloadName);
            SendToUTorrent(downloadPath, torrentFile, downloadName);
        }

        static void LoadConfig()
        {
            //Load the Config here
            _logDir = new DirectoryInfo(ConfigurationManager.AppSettings["logDir"]); //Get logDir from app.config
            _logFile = _logDir + PATH_SEP + "Sab2Torrent.txt"; // Log File

            _torrentDir = new DirectoryInfo(ConfigurationManager.AppSettings["torrentDirectory"]);
            _uTorrentExe = new FileInfo(ConfigurationManager.AppSettings["uTorrentExe"]); //Get uTorrent EXE location from app.config

            _uploadToServer = Convert.ToBoolean(ConfigurationManager.AppSettings["uploadToServer"]);
            _addToUTorrent = Convert.ToBoolean(ConfigurationManager.AppSettings["addToUTorrent"]);
            _torrentPrivate = Convert.ToBoolean(ConfigurationManager.AppSettings["addToUTorrent"]);

            _torrentTrackers = new List<string>();

            //Get List of trackers in config file
            foreach (var tracker in ConfigurationManager.AppSettings["torrentTrackers"].Split(';'))
            {
                _torrentTrackers.Add(tracker);
            }
        }

        static void CreateTorrent(string path, string torrentFile, string torrentName)
        {
            Log("Creating the Torrent for {0}", torrentName);
            //Create the torrent with a command line application (monotorrent???)
            TorrentCreator tc = new TorrentCreator();

            tc.Announces.Add(_torrentTrackers); //Add Trackers to Torrent

            //Add Comments :)
            tc.CreatedBy = "Sab2Torrent";
            tc.Publisher = "http://code.google.com/p/sabscripts/";

            tc.Path = path; //Add the path (folder) to Hash and add to the torrent

            //Print the Progress to the command line (mostly for debugging)
            tc.Hashed += delegate (object obj, TorrentCreatorEventArgs e) {
            Console.WriteLine("Overall {0:f}% hashed", e.OverallCompletion);
            };

            if (_torrentPrivate) //If Config says torrent should be private, make it so!
                tc.Private = true;

            tc.Create(torrentFile); //Create the Torrent File
            Log("Torrent file created: {0}", torrentFile);
        }

        static void UploadTorrent(string path)
        {
            //Send the torrent to a webserver (http or ftp?)
        }

        static void SendToUTorrent(string path, string torrentFile, string torrentName)
        {
            //Send the torrent to uTorrent for seeding
            //How is this done? Command Line? WebUI?
            //uTorrent.exe /DIRECTORY "SAVE PATH" "PATH TO .TORRENT FILE" - EASY!

            //If uTorrent.exe exists in the defined location, do more
            if (_uTorrentExe.Exists)
            {
                //Run the App
                string commandArgs = String.Format("/DIRECTORY \"{0}\" \"{1}\"", path, torrentFile);
                //Create string to hold commands passed to uTorrent.exe
                Log("Sending {0} to uTorrent", torrentName);
                Process.Start(_uTorrentExe.ToString(), commandArgs).WaitForExit();
                Log("Torrent sent to uTorrent");
            }

            else
                Log("uTorrent.exe is not in location defined!!");
        }

        private static string LastEntry(string text, string sep)
        {
            string[] tmp = text.Split(sep.ToCharArray());
            return tmp[tmp.Length - 1];
        }

        private static void Log(string message)
        {
            Console.WriteLine(message);

            try
            {
                using (StreamWriter sw = File.AppendText(_logFile))
                {
                    sw.WriteLine(message);
                }
            }
            catch { }
        }

        private static void Log(string message, params object[] para)
        {

            Log(String.Format(message, para));
        }
    }
}