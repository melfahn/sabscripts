using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace Movies
{
    class Program
    {
        private static string PATH_SEP = Path.DirectorySeparatorChar.ToString();

        private static DirectoryInfo _logDir;
        private static string _logFile;
        private static DirectoryInfo _movieDir;
        private static string _xbmcMoviePath;
        private static DirectoryInfo _hdMovieDir;
        private static string _xbmcHdMoviePath;
        private static DirectoryInfo _ipodMovieDir;
        private static string _xbmcHost;
        private static int _xbmcPort = 9777;
        private static bool _updateXbmc;
        private static bool _notifyXbmc;
        private static bool _cleanLibrary;
        private static bool _xbmcOsWindows;
        private static bool _deleteFolder;
        private static bool _overwriteFile;
        private static bool _imdb;
        private static string _mencoderOptions = "-forceidx -ovc copy -oac copy -o"; //Options for mencoder (static)

        static void Main(string[] args)
        {
            LoadConfig();

            string moviePath = args[0]; //Get moviePath from first CMD Line argument

            if (moviePath.EndsWith(PATH_SEP))
            {
                moviePath = moviePath.Substring(0, moviePath.Length - 2);
            }

            // if passed with just one arg (not just sabnzbd passing), make movienamesab last directory entry
            string movieNameSab = (args.Length == 1) ? LastEntry(moviePath, PATH_SEP) : args[2]; //Get movieName from third CMD Line argument

            string movieName = GetMovieName(movieNameSab);

            // try to append imdb id to end of filename if found in nfo
            if (_imdb)
            {
                try
                {
                    var f = Directory.GetFiles(moviePath, "*.nfo");
                    var r = new StreamReader(f[0]);
                    string txt = r.ReadToEnd();
                    r.Close();
                    r = null;
                    f = null;

                    Match m = null;
                    if ((m = Regex.Match(txt.Replace('\n', ' '), @"(tt|title\?)(\d{7})", (RegexOptions.IgnoreCase))).Success)
                    {
                        movieName += String.Format(" [tt{0}]", m.Groups[2].Captures[0].Value);
                    }

                }
                catch (Exception)
                {
                    // do nothing!!
                }
            }

            try //Try/Catch for Deleting Samples
            {
                // also deletes sfv ssr nzb etc, anything under 140MB
                DeleteSamples(moviePath);
            }
            catch (Exception ex)
            {
                Log(ex.ToString()); //Log Exception for deleting samples + other small files
                throw;
            }

            // moviePathInfo = new DirectoryInfo(moviePath);

            try //try catch for processing files, Only one is performed, saves on multiple try catch blocks - if something fails, all should be deleted
            {
                if (Directory.GetFiles(moviePath, "*.avi", SearchOption.AllDirectories).Length > 0)
                {
                    Log("Processing AVI Movie");
                    ProcessAvi(moviePath, movieName);
                    return;
                }

                if (Directory.GetFiles(moviePath, "*.mkv", SearchOption.AllDirectories).Length > 0)
                {
                    Log("Processing MKV Moveie");
                    ProcessMkv(moviePath, movieName);
                    return;
                }

                if (Directory.GetFiles(moviePath, "*.wmv", SearchOption.AllDirectories).Length > 0)
                {
                    Log("Procesing WMV Movie");
                    ProcessWmv(moviePath, movieName);
                    return;
                }

                if (Directory.GetFiles(moviePath, "*.mp4", SearchOption.AllDirectories).Length > 0)
                {
                    Log("Processing MP4 Movie");
                    ProcessMp4(moviePath, movieName);
                    return;
                }
            }

            catch (Exception ex)
            {
                Log(ex.ToString()); //Log the exception
            }
        }

        private static string LastEntry(string text, string sep)
        {
            string[] tmp = text.Split(sep.ToCharArray());
            return tmp[tmp.Length - 1];
        }

        private static void LoadConfig()
        {
            _logDir = new DirectoryInfo(ConfigurationManager.AppSettings["logDir"]); //Get logDir from app.config
            _logFile = _logDir + PATH_SEP + "Movies.txt"; // Log File

            _movieDir = new DirectoryInfo(ConfigurationManager.AppSettings["movieDir"]);
            _hdMovieDir = new DirectoryInfo(ConfigurationManager.AppSettings["HdMovieDir"]);
            _ipodMovieDir = new DirectoryInfo(ConfigurationManager.AppSettings["ipodMovieDir"]);

            _xbmcMoviePath = ConfigurationManager.AppSettings["xbmcMoviePath"];
            _xbmcHdMoviePath = ConfigurationManager.AppSettings["xbmcHdMoviePath"];

            _xbmcHost = ConfigurationManager.AppSettings["xbmcHost"];
            string xbmcPortS = ConfigurationManager.AppSettings["xbmcPort"];
            int.TryParse(xbmcPortS, out _xbmcPort);

            _updateXbmc = Convert.ToBoolean(ConfigurationManager.AppSettings["updateXbmc"]);
            _notifyXbmc = Convert.ToBoolean(ConfigurationManager.AppSettings["notifyXbmc"]);
            _cleanLibrary = Convert.ToBoolean(ConfigurationManager.AppSettings["cleanLibrary"]);

            _xbmcOsWindows = Convert.ToBoolean(ConfigurationManager.AppSettings["xbmcOsWindows"]);

            _deleteFolder = Convert.ToBoolean(ConfigurationManager.AppSettings["deleteFolder"]);
            _overwriteFile = Convert.ToBoolean(ConfigurationManager.AppSettings["overwriteFile"] ?? "false");
            _imdb = Convert.ToBoolean(ConfigurationManager.AppSettings["imdb"]);
        }

        private static string UpdateXbmc(string moviePath, string movieName, string category)
        {
            Log("Updating XBMC");

            string xbmcUpdating = null;
            string xbmcPath = null;
            string messageHeader = "Movie Downloaded";

            if (category == "standard")
            {
                if (_movieDir.ToString().ToLower() != _xbmcMoviePath.ToLower())
                {
                    xbmcPath = _movieDir.ToString().Replace(_movieDir.ToString(), _xbmcMoviePath);

                    if (!_xbmcOsWindows)
                        xbmcPath = xbmcPath.Replace('\\', '/');
                }

                else
                {
                    xbmcPath = moviePath;
                }
            }

            else if (category == "highdef")
            {
                messageHeader = "HD Movie Downloaded";

                if (_hdMovieDir.ToString().ToLower() != _xbmcHdMoviePath.ToLower())
                {
                    xbmcPath = _hdMovieDir.ToString().Replace(_hdMovieDir.ToString(), _xbmcHdMoviePath);

                    if (!_xbmcOsWindows)
                        xbmcPath = xbmcPath.Replace('\\', '/');
                }

                else
                {
                    xbmcPath = moviePath;
                }
            }

            else
            {
                Console.WriteLine("Format is unknown...");
            }

            Log("Connecting to XBMC...");
            if (!XBMC.EventClient.Current.Connected)
                XBMC.EventClient.Current.Connect(_xbmcHost, _xbmcPort);

            if (XBMC.EventClient.Current.Connected)
            {
                string xbmcLibraryUpdate = "UpdateLibrary(video," + xbmcPath + ")";

                XBMC.EventClient.Current.SendAction(xbmcLibraryUpdate, "");
                if (_notifyXbmc)
                    XBMC.EventClient.Current.SendNotification(messageHeader, movieName, XBMC.IconType.ICON_PNG, "sabnzbd");
                if (_cleanLibrary)
                    XBMC.EventClient.Current.SendAction("CleanLibrary(video)", "");
                xbmcUpdating = "XBMC is Updating";
                return xbmcUpdating;
            }

            xbmcUpdating = "Could not connect to XBMC";
            return xbmcUpdating;
        }

        private static string GetMovieName(string movieNameSab)
        {
            string movieName = movieNameSab;
            string patternYear = @"(?<Year>\d{4})";

            Match titleMatchYear = Regex.Match(movieNameSab, patternYear);

            if (titleMatchYear.Success)
            {
                int year = 0;
                Int32.TryParse(titleMatchYear.Groups["Year"].Value, out year);

                string[] titleSplit = Regex.Split(movieNameSab, patternYear);

                movieName = titleSplit[0].TrimEnd(' ', '(', '.').Replace('.', ' ') + " (" + year + ")";
            }

            return movieName;
        }

        private static void DeleteSamples(string moviePath)
        {
            string[] filesSizeTest = Directory.GetFiles(moviePath, "*.*", SearchOption.AllDirectories);
            //Search moviePath for all Files, including sub-folders
            Array.Sort(filesSizeTest); //Sort Array :)

            foreach (string fileSizeTest in filesSizeTest) //Check for sample files that SAB may have missed
            {
                FileInfo fileSizeTestInfo = new FileInfo(fileSizeTest); //Create FileInfo
                if (fileSizeTestInfo.Length < 140000000)
                //If File is less than 140MB consider it a sample (For HD Samples)
                {
                    File.Delete(fileSizeTest); //Delete the sample file
                }
            }
        }

        private static void DeleteExistingFile(string path)
        {
            Log("Deleting existing movie: {0}", path);
            File.Delete(path);
        }

        private static void ProcessAvi(string moviePath, string movieName)
        {
            string movieFilename;

            movieFilename = Path.Combine(_movieDir.FullName, movieName + ".avi");
            
            string[] aviFiles = Directory.GetFiles(moviePath, "*.avi", SearchOption.AllDirectories);
            //Search moviePath for AVI Files, including sub-folders
            Array.Sort(aviFiles); //Sort Array :)

            if (aviFiles.Length == 1) //If Only one AVI was found rename and move file
            {
                Log("Single AVI found, Processing...");
                string aviFile = aviFiles[0]; //Set aviFile to first & only file in aviFiles Array
                FileInfo aviFileInfo = new FileInfo(aviFile); //Create file info for getting file size
                long aviSize = aviFileInfo.Length; //Create long to store AVI File Size
                if (aviSize > 150000000) //If avi is greater than 150MB then continue
                {
                    if (_deleteFolder)
                    {
                        if (_overwriteFile && File.Exists(movieFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(movieFilename);

                        //File.Move(aviFile, movieFilename); //Move/Rename File
                        aviFileInfo.MoveTo(movieFilename);
                        Log("{0} Moved",movieName);
                        Directory.Delete(moviePath, true); //Delete directory + all files
                        Log("Folder Deleted");
                    }

                    else
                    {
                        string aviMovieDir = _movieDir + PATH_SEP.ToString() + movieName;
                        string aviMovieDirFilename = aviMovieDir + PATH_SEP.ToString() + movieName + ".avi";

                        if (_overwriteFile && File.Exists(aviMovieDirFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(aviMovieDirFilename);

                        Directory.CreateDirectory(aviMovieDir);
                        aviFileInfo.MoveTo(aviMovieDirFilename);
                        Directory.Delete(moviePath, true); //Delete old directory + all files
                        Log("{0} moved, Folder was kept", movieName);
                    }

                    if (_updateXbmc)
                    {
                        string xbmcUpdate = UpdateXbmc(_movieDir.ToString(), movieName, "standard");
                        //string xbmcUpdate = UpdateXbmc();
                    }
                    return; //Exit
                }
            }

            else if (aviFiles.Length == 2) //If two AVIs were found
            {
                Log("Two AVI's Found, processing...");
                string aviFileOne = aviFiles[0]; //Name aviFileOne to first file in Array
                string aviFileTwo = aviFiles[1]; //Name aviFileTwo to second file in Array
                FileInfo aviFileOneInfo = new FileInfo(aviFileOne); //Create FileInfo for aviFileOne
                FileInfo aviFileTwoInfo = new FileInfo(aviFileTwo); //Create FileInfo for aviFileTwo

                long aviOneSize = aviFileOneInfo.Length; //Create long to store file size
                long aviTwoSize = aviFileTwoInfo.Length; //Create long to store file size

                if (aviOneSize > 150000000 && aviTwoSize > 150000000) //Ensure that both files are actually
                {
                    if (_deleteFolder)
                    {
                        string mencoderCommand = _mencoderOptions + " \"" + movieFilename + "\" \"" + aviFileOne +
                                                 "\" \"" + aviFileTwoInfo + "\"";
                        //Create string to hold commands to pass to mencoder

                        if (_overwriteFile && File.Exists(movieFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(movieFilename);

                        Log("Running mencoder on {0}", movieName);
                        Process.Start("mencoder.exe", mencoderCommand).WaitForExit();
                        //Run mencoder on the two AVIs & wait for finish
                        Directory.Delete(moviePath, true); //Delete directory + all files
                        Log("Folder deleted");
                    }

                    else
                    {
                        string aviMovieDir = Path.Combine(_movieDir.FullName, movieName);
                        movieFilename = Path.Combine(aviMovieDir, movieName + ".avi");
                        Directory.CreateDirectory(aviMovieDir);

                        //Create string to hold commands to pass to mencoder
                        string mencoderCommand = _mencoderOptions + " \"" + movieFilename + "\" \"" + aviFileOne +
                                                 "\" \"" + aviFileTwoInfo + "\"";  

                        if (_overwriteFile && File.Exists(movieFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(movieFilename);

                        Log("Running mencoder on {0}", movieName);
                        //Run mencoder on the two AVIs & wait for finish
                        Process.Start("mencoder.exe", mencoderCommand).WaitForExit();
                        Directory.Delete(moviePath, true); //Delete old directory + all files
                        Log("{0} moved, Folder was kept", movieName);
                    }
                    if (_updateXbmc)
                    {
                        string xbmcUpdate = UpdateXbmc(_movieDir.ToString(), movieName, "standard");
                        //string xbmcUpdate = UpdateXbmc();
                    }
                    return; //Exit
                }
            }
            return;
        }

        private static void ProcessMkv(string moviePath, string movieName)
        {
            string movieFilename;

            if (_hdMovieDir.ToString().EndsWith(PATH_SEP.ToString()))
                movieFilename = _hdMovieDir + movieName + ".mkv"; //Create movieFilename from movieDir + movieName

            else
                movieFilename = _hdMovieDir + PATH_SEP.ToString() + movieName + ".mkv"; //Create movieFilename from movieDir + movieName

            string[] mkvFiles = Directory.GetFiles(moviePath, "*.mkv", SearchOption.AllDirectories);
            //Search moviePath for MKV Files, including sub-folders
            Array.Sort(mkvFiles); //Sort Array :)

            if (mkvFiles.Length == 1) //Ensure only one MKV was found
            {
                string mkvFile = mkvFiles[0]; //Create mkvFile from first & only string in Array
                FileInfo mkvFileInfo = new FileInfo(mkvFile); //Create FileInfo

                if (mkvFileInfo.Length > 120000000) //Ensure MKV is over 1200MB in size
                {
                    Log("Processing Single MKV...");
                    if (_deleteFolder)
                    {
                        if (_overwriteFile && File.Exists(movieFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(movieFilename);

                        mkvFileInfo.MoveTo(movieFilename);
                        Log("{0} Moved", movieName);
                        Directory.Delete(moviePath, true); //Delete directory + all files
                        Log("Folder was deleted");
                    }

                    else
                    {
                        string mkvMovieDir = _hdMovieDir + PATH_SEP.ToString() + movieName;
                        string mkvMovieDirFilename = mkvMovieDir + PATH_SEP.ToString() + movieName + ".mkv";

                        if (_overwriteFile && File.Exists(mkvMovieDirFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(mkvMovieDirFilename);

                        Directory.CreateDirectory(mkvMovieDir);
                        mkvFileInfo.MoveTo(mkvMovieDirFilename);
                        Log("{0} Moved, folder was kept", movieName);
                        Directory.Delete(moviePath, true); //Delete old directory + all files   
                    }
                    if (_updateXbmc)
                    {
                        string xbmcUpdate = UpdateXbmc(_hdMovieDir.ToString(), movieName, "highdef");
                    }
                    return; //Exit
                }
            }

            return;
        }

        private static void ProcessWmv(string moviePath, string movieName)
        {
            string movieFilename;

            if (_hdMovieDir.ToString().EndsWith(PATH_SEP.ToString()))
                movieFilename = _hdMovieDir + movieName + ".wmv"; //Create movieFilename from movieDir + movieName

            else
                movieFilename = _hdMovieDir + PATH_SEP.ToString() + movieName + ".wmv"; //Create movieFilename from movieDir + movieName

            string[] wmvFiles = Directory.GetFiles(moviePath, "*.wmv", SearchOption.AllDirectories);
            //Search moviePath for WMV Files, including sub-folders
            Array.Sort(wmvFiles); //Sort Array :)

            if (wmvFiles.Length == 1) //Ensure only one WMV was found
            {
                string wmvFile = wmvFiles[0]; //Create wmvFile from first & only string in Array
                FileInfo wmvFileInfo = new FileInfo(wmvFile); //Create FileInfo

                if (wmvFileInfo.Length > 120000000) //Ensure WMV is over 1200MB in size
                {
                    Log("Processing Single WMV");
                    if (_deleteFolder)
                    {
                        if (_overwriteFile && File.Exists(movieFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(movieFilename);

                        wmvFileInfo.MoveTo(movieFilename);
                        Log("{0} Moved", movieName);
                        Directory.Delete(moviePath, true); //Delete directory + all files
                        Log("Folder was deleted");
                    }

                    else
                    {
                        string wmvMovieDir = _hdMovieDir + PATH_SEP.ToString() + movieName;
                        string wmvMovieDirFilename = wmvMovieDir + PATH_SEP.ToString() + movieName + ".wmv";

                        if (_overwriteFile && File.Exists(wmvMovieDirFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(wmvMovieDirFilename);

                        Directory.CreateDirectory(wmvMovieDir);
                        wmvFileInfo.MoveTo(wmvMovieDirFilename);
                        Log("{0} Moved, folder was kept", movieName);
                        Directory.Delete(moviePath, true);
                    }

                    if (_updateXbmc)
                    {
                        string xbmcUpdate = UpdateXbmc(_hdMovieDir.ToString(), movieName, "highdef");
                    }
                }
            }

            return;
        }

        private static void ProcessMp4(string moviePath, string movieName)
        {
            string movieFilename;

            if (_ipodMovieDir.ToString().EndsWith(PATH_SEP.ToString()))
                movieFilename = _ipodMovieDir + movieName + ".mp4"; //Create movieFilename from movieDir + movieName

            else
                movieFilename = _ipodMovieDir + PATH_SEP.ToString() + movieName + ".mp4"; //Create movieFilename from movieDir + movieName

            string[] mp4Files = Directory.GetFiles(moviePath, "*.mp4", SearchOption.AllDirectories);
            //Search moviePath for MP4 Files, including sub-folders
            Array.Sort(mp4Files); //Sort Array :)

            if (mp4Files.Length == 1) //Ensure only one MP4 was found
            {
                string mp4File = mp4Files[0]; //Create wmvFile from first & only string in Array
                FileInfo mp4FileInfo = new FileInfo(mp4File); //Create FileInfo

                if (mp4FileInfo.Length > 100000000) //Ensure MP4 is over 100MB in size
                {
                    Log("Processing Single MP4...");
                    if (_deleteFolder)
                    {
                        if (_overwriteFile && File.Exists(movieFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(movieFilename);

                        mp4FileInfo.MoveTo(movieFilename);
                        Log("{0} Moved", movieName);
                        Directory.Delete(moviePath, true); //Delete directory + all files
                        Log("Directory was deleted");
                    }

                    else
                    {
                        movieFilename = moviePath + PATH_SEP.ToString() + movieName + ".mp4";
                        File.Move(mp4File, movieFilename);
                        string mp4MovieDir = _ipodMovieDir + PATH_SEP.ToString() + movieName;
                        string mp4MovieDirFilename = mp4MovieDir + PATH_SEP.ToString() + movieName + ".mp4";

                        if (_overwriteFile && File.Exists(mp4MovieDirFilename)) //If overwriteFile & File already exists, then delete it
                            DeleteExistingFile(mp4MovieDirFilename);

                        Directory.CreateDirectory(mp4MovieDir);
                        mp4FileInfo.MoveTo(mp4MovieDirFilename);
                        Log("{0} Moved, folder was kept", movieName);
                        Directory.Delete(moviePath, true);
                    }
                    return; //Exit
                }
            }

            return;
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
