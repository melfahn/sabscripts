using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SABSync.Properties;

namespace SABSync
{
    public class Config
    {
        private static readonly Logger Logger = new Logger();
        private IList<FeedInfo> _feeds;
        private IList<string> _myShows;
        private DirectoryInfo _nzbDir;
        private IList<ShowAlias> _showAliases;
        private IList<DirectoryInfo> _tvRootFolders;

        private string _sabRequest;

        public bool DownloadPropers { get; set; }
        public bool SyncOnStart { get; set; }
        public bool SabReplaceChars { get; set; }
        public bool VerboseLogging { get; set; }
        
        public int Interval { get; set; }
        public int DeleteLogs { get; set; }

        public string TvTemplate { get; set; }
        public string TvDailyTemplate { get; set; }

        public string[] VideoExt { get; set; }
        public string[] DownloadQualities { get; set; }

        public string SabRequest
        {
            get { return _sabRequest ?? (_sabRequest = GetSabRequest()); }
            set { _sabRequest = value; }
        }
        
        public Config()
        {
            LoadValues();
        }

        public IList<FeedInfo> Feeds
        {
            get { return _feeds ?? (_feeds = GetFeeds()); }
            set { _feeds = value; }
        }

        public IList<ShowAlias> ShowAliases
        {
            get { return _showAliases ?? (_showAliases = GetShowAliases()); }
            set { _showAliases = value; }
        }

        public IList<DirectoryInfo> TvRootFolders
        {
            get { return _tvRootFolders ?? (_tvRootFolders = GetTvRootFolders()); }
            set { _tvRootFolders = value; }
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

        private DirectoryInfo GetNzbDir()
        {
            string path = Settings.Default.NzbDir;
            if (string.IsNullOrEmpty(path)) return null;

            var folder = new DirectoryInfo(path);
            if (!folder.Exists)
                throw new ApplicationException(string.Format("Configuration: Invalid nzbDir folder: {0}", folder));

            return folder;
        }

        private IList<ShowAlias> GetShowAliases()
        {
            //Get from DB
            IList<ShowAlias> aliasList = new List<ShowAlias>();
            using (SABSyncEntities sabSyncEntities = new SABSyncEntities())
            {
                var aliases = from a in sabSyncEntities.shows
                              where !String.IsNullOrEmpty(a.aliases)
                              select new { a.show_name, a.aliases };

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

        private IList<DirectoryInfo> GetTvRootFolders()
        {
            return (from path in Settings.Default.TvRoot.Trim(';').Split(';')
                    select new DirectoryInfo(path)
                        into folder
                        where folder.Exists
                        select folder).ToList();
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

        private string GetSabRequest()
        {
            string sabnzbdInfo = Settings.Default.SabnzbdInfo;
            int priority = Settings.Default.Priority;
            string apiKey = Settings.Default.ApiKey;
            string username = Settings.Default.Username;
            string password = Settings.Default.Password;
            return string.Format(
                "http://{0}/sabnzbd/api?$Action&priority={1}&apikey={2}&ma_username={3}&ma_password={4}",
                sabnzbdInfo, priority, apiKey, username, password).Replace("$Action", "{0}");
        }

        private static bool IsExcluded(DirectoryInfo folder)
        {
            bool isSystem = (folder.Attributes & FileAttributes.System) == FileAttributes.System;
            bool isHidden = (folder.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
            return isSystem || isHidden;
        }

        public void SaveValue(string element, object value)
        {
            //Save the value to the element
            try
            {
                Settings.Default[element] = value;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }

        public object GetValue(string element)
        {
            //Save the value to the element
            try
            {
                return Settings.Default[element];
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            return null;
        }

        public void SaveConfig()
        {
            Settings.Default.Save();
        }

        public void ReloadConfig()
        {
            Settings.Default.Reload();
        }

        private void LoadValues()
        {
            DownloadPropers = Settings.Default.DownloadPropers;
            SyncOnStart = Settings.Default.SyncOnStart;
            SabReplaceChars = Settings.Default.SabReplaceChars;
            VerboseLogging = Settings.Default.VerboseLogging;

            Interval = Settings.Default.Interval;
            DeleteLogs = Settings.Default.DeleteLogs;

            TvTemplate = Settings.Default.TvTemplate;
            TvDailyTemplate = Settings.Default.TvDailyTemplate;

            VideoExt = Settings.Default.VideoExt.Split(';');
            DownloadQualities = Settings.Default.DownloadQuality.Split(';');
        }
    }
}
