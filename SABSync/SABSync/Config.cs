using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SABSync
{
    public class Config
    {
        private static readonly Logger Logger = new Logger();
        private IList<FeedInfo> _feeds;
        private IList<string> _myShows;
        private DirectoryInfo _nzbDir;
        private string _sabRequest;
        private IList<ShowAlias> _showAliases;
        private string _tvDailyTemplate;
        private IList<DirectoryInfo> _tvRootFolders;
        private string _tvTemplate;

        private string configFile = @"Settings.xml"; //Used to load/save the config file

        public Config()
        {
            LoadConfig();
        }

        public bool DownloadPropers { get; set; }

        public bool SyncOnStart { get; set; }

        public string[] DownloadQualities { get; set; }

        public int Interval { get; set; }

        public IList<FeedInfo> Feeds
        {
            get { return _feeds ?? (_feeds = GetFeeds()); }
            set { _feeds = value; }
        }

        public IList<string> MyShows
        {
            get { return _myShows ?? (_myShows = GetMyShows()); }
            set { _myShows = value; }
        }

        public DirectoryInfo NzbDir
        {
            get { return _nzbDir ?? (_nzbDir = GetNzbDir()); }
            set { _nzbDir = value; }
        }

        public string SabRequest
        {
            get { return _sabRequest ?? (_sabRequest = GetSabRequest()); }
            set { _sabRequest = value; }
        }

        public bool SabReplaceChars { get; set; }

        public IList<ShowAlias> ShowAliases
        {
            get { return _showAliases ?? (_showAliases = GetShowAliases()); }
            set { _showAliases = value; }
        }

        public string TvDailyTemplate
        {
            get { return _tvDailyTemplate ?? (_tvDailyTemplate = GetTvDailyTemplate()); }
            set { _tvDailyTemplate = value; }
        }

        public IList<DirectoryInfo> TvRootFolders
        {
            get { return _tvRootFolders ?? (_tvRootFolders = GetTvRootFolders()); }
            set { _tvRootFolders = value; }
        }

        public string TvTemplate
        {
            get { return _tvTemplate ?? (_tvTemplate = GetTvTemplate()); }
            set { _tvTemplate = value; }
        }

        public bool VerboseLogging { get; set; }

        public string[] VideoExt { get; set; }

        public Hashtable Settings { get; private set; }

        private string GetSabRequest()
        {
            string sabnzbdInfo = GetValue("SabNzbdInfo");
            string priority = GetValue("Priority");
            string apiKey = GetValue("ApiKey");
            string username = GetValue("Username");
            string password = GetValue("Password");
            return string.Format(
                "http://{0}/sabnzbd/api?$Action&priority={1}&apikey={2}&ma_username={3}&ma_password={4}",
                sabnzbdInfo, priority, apiKey, username, password).Replace("$Action", "{0}");
        }

        private string GetTvTemplate()
        {
            string setting = GetValue("TvTemplate");
            if (string.IsNullOrEmpty(setting))
                throw new ApplicationException("Configuration missing: tvTemplate");
            return setting;
        }

        private string GetTvDailyTemplate()
        {
            string setting = GetValue("TvDailyTemplate");
            if (string.IsNullOrEmpty(setting))
                throw new ApplicationException("Configuration missing: tvDailyTemplate");
            return setting;
        }

        private DirectoryInfo GetNzbDir()
        {
            string path = GetValue("NzbDir");
            if (string.IsNullOrEmpty(path)) return null;

            var folder = new DirectoryInfo(path);
            if (!folder.Exists)
                throw new ApplicationException(string.Format("Configuration: Invalid nzbDir folder: {0}", folder));

            return folder;
        }

        private IList<string> GetMyShows()
        {
            var list = new List<string>();
            foreach (DirectoryInfo rootFolder in TvRootFolders)
            {
                if (VerboseLogging)
                {
                    Logger.Log(string.Empty);
                    Logger.Log("DEBUG: TVRoot Directory: {0}", rootFolder);
                }

                foreach (DirectoryInfo folder in rootFolder.GetDirectories())
                {
                    string show = folder.Name;

                    if (IsExcluded(folder) || list.Contains(show))
                        continue;

                    if (VerboseLogging) Logger.Log("DEBUG: Show: {0}", show);
                    list.Add(show);
                }
            }
            return list;
        }

        private static bool IsExcluded(DirectoryInfo folder)
        {
            bool isSystem = (folder.Attributes & FileAttributes.System) == FileAttributes.System;
            bool isHidden = (folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
            return isSystem || isHidden;
        }

        private IList<DirectoryInfo> GetTvRootFolders()
        {
            return (from path in GetValue("TvRoot").Trim(';').Split(';')
                    select new DirectoryInfo(path)
                    into folder
                    where folder.Exists
                    select folder).ToList();
        }

        private IList<FeedInfo> GetFeeds()
        {
            //Load from DB
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                IList<FeedInfo> fi = new List<FeedInfo>();

                foreach (var feed in from f in sabSyncEntities.providers select new { f.name, f.url })
                    fi.Add(new FeedInfo(feed.name, feed.url));

                return fi;
            }
        }

        private IList<ShowAlias> GetShowAliases()
        {
            //Get from DB
            IList<ShowAlias> aliasList = new List<ShowAlias>();
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {

                var aliases = from a in sabSyncEntities.shows
                              where !String.IsNullOrEmpty(a.aliases)
                              select new {a.show_name, a.aliases};

                foreach (var alias in aliases)
                {
                    foreach (var badName in alias.aliases.Split(';'))
                    {
                        ShowAlias showAlias = new ShowAlias();
                        showAlias.Alias = alias.show_name;
                        showAlias.BadName = badName;
                        aliasList.Add(showAlias);
                    }
                }
            }

            return aliasList;
        }

        private void LoadConfig()
        {
            //Read the XML file
            XDocument xDoc = XDocument.Load(configFile);
            var config = (from c in xDoc.Descendants("Configuration") select c).FirstOrDefault();
            Hashtable ht = new Hashtable();

            foreach (var c in config.Descendants())
            {
                ht.Add(c.Name, c.Value);
            }

            Settings = ht;

            LoadValues(); //Load values to Variables
        }

        private void LoadValues()
        {
            DownloadPropers = Convert.ToBoolean(GetValue("DownloadPropers") ?? "false");
            Interval = Convert.ToInt32(GetValue("Interval") ?? "15");
            DownloadQualities = GetValue("DownloadQuality").Trim(';', ' ').Split(';');
            SabReplaceChars = Convert.ToBoolean(GetValue("SabReplaceChars") ?? "false");
            VerboseLogging = Convert.ToBoolean(GetValue("VerboseLogging") ?? "false");
            VideoExt = (GetValue("VideoExt") ?? string.Empty).Trim(';', ' ').Split(';');
            SyncOnStart = Convert.ToBoolean(GetValue("SyncOnStart") ?? "false");
        }

        public void ReloadConfig()
        {
            //Used to Reload the Configuration from an Updated file...
            LoadConfig();
        }

        public void SaveValue(string element, string value)
        {
            //Save the value to the element
            try
            {
                XDocument xDoc = XDocument.Load(configFile);

                var config = (from c in xDoc.Descendants("Configuration") select c).FirstOrDefault();
                config.Element(element).Value = value;
                config.Save(configFile);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public string GetValue(string element)
        {
            //Save the value to the element
            try
            {
                XDocument xDoc = XDocument.Load(configFile);

                var config = (from c in xDoc.Descendants("Configuration") select c).FirstOrDefault();
                return config.Element(element).Value;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            return null;
        }
    }
}
