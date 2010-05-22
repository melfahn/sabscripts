using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;

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
        private IList<ShowQuality> _showQualities;
        private string _tvDailyTemplate;
        private IList<DirectoryInfo> _tvRootFolders;
        private string _tvTemplate;

        public Config() : this(ConfigurationManager.AppSettings)
        {
        }

        public Config(NameValueCollection settings)
        {
            Settings = settings;

            DownloadPropers = Convert.ToBoolean(Settings["downloadPropers"] ?? "false");
            DownloadQualities = (Settings["downloadQuality"] ?? string.Empty).Trim(';', ' ').Split(';');
            IgnoreSeasons = Settings["ignoreSeasons"] ?? string.Empty;
            SabReplaceChars = Convert.ToBoolean(Settings["sabReplaceChars"] ?? "false");
            VerboseLogging = Convert.ToBoolean(Settings["verboseLogging"] ?? "false");
            VideoExt = (Settings["videoExt"] ?? string.Empty).Trim(';', ' ').Split(';');
        }

        public bool DownloadPropers { get; set; }

        public string[] DownloadQualities { get; set; }

        public IList<FeedInfo> Feeds
        {
            get { return _feeds ?? (_feeds = GetFeeds()); }
            set { _feeds = value; }
        }

        public string IgnoreSeasons { get; set; }

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

        public IList<ShowQuality> ShowQualities
        {
            get { return _showQualities ?? (_showQualities = GetShowQualities()); }
            set { _showQualities = value; }
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

        public NameValueCollection Settings { get; private set; }

        private string GetSabRequest()
        {
            string sabnzbdInfo = GetSetting("sabnzbdInfo");
            string priority = GetSetting("priority");
            string apiKey = GetSetting("apiKey");
            string username = GetSetting("username");
            string password = GetSetting("password");
            return string.Format(
                "http://{0}/api?$Action&priority={1}&apikey={2}&ma_username={3}&ma_password={4}",
                sabnzbdInfo, priority, apiKey, username, password).Replace("$Action", "{0}");
        }

        private string GetTvTemplate()
        {
            string setting = GetSetting("tvTemplate");
            if (string.IsNullOrEmpty(setting))
                throw new ApplicationException("Configuration missing: tvTemplate");
            return setting;
        }

        private string GetTvDailyTemplate()
        {
            string setting = GetSetting("tvDailyTemplate");
            if (string.IsNullOrEmpty(setting))
                throw new ApplicationException("Configuration missing: tvDailyTemplate");
            return setting;
        }

        private DirectoryInfo GetNzbDir()
        {
            string path = Settings["nzbDir"];
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
                if (VerboseLogging) Logger.Log("TVRoot Directory: {0}", rootFolder);

                foreach (DirectoryInfo folder in rootFolder.GetDirectories())
                {
                    string show = folder.Name;

                    if (IsExcluded(folder) || list.Contains(show))
                        continue;

                    if (VerboseLogging) Logger.Log("Adding show to wanted shows list: {0}", show);
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
            return (from path in GetSetting("tvRoot").Trim(';').Split(';')
                    select new DirectoryInfo(path)
                    into folder
                    where folder.Exists
                    select folder).ToList();
        }

        private IList<FeedInfo> GetFeeds()
        {
            FileInfo configFile = GetConfigFile("rss");
            Logger.Log("Loading RSS feed list from {0}", configFile);

            return (from pair in GetPipeDelimitedPairs(configFile)
                    let name = pair.Value == null ? null : pair.Key
                    let url = pair.Value ?? pair.Key
                    select new FeedInfo(name, url)).ToList();
        }

        private IList<ShowAlias> GetShowAliases()
        {
            return (from pair in GetPipeDelimitedPairs(GetConfigFile("alias"))
                    select new ShowAlias {BadName = pair.Key, Alias = pair.Value}).ToList();
        }

        private IList<ShowQuality> GetShowQualities()
        {
            return (from pair in GetPipeDelimitedPairs(GetConfigFile("quality"))
                    select new ShowQuality {Name = pair.Key, Quality = pair.Value}).ToList();
        }

        private FileInfo GetConfigFile(string key)
        {
            string path = GetSetting(key);
            var file = new FileInfo(path);
            if (!file.Exists)
                throw new ApplicationException(string.Format("File not found. {0}", path));
            return file;
        }

        private string GetSetting(string key)
        {
            string value = Settings[key];
            if (value == null)
                throw new ApplicationException("Configuration missing: " + key);
            return value;
        }

        private static IEnumerable<KeyValuePair<string, string>> GetPipeDelimitedPairs(FileInfo file)
        {
            return from line in File.ReadAllLines(file.FullName)
                   where !line.StartsWith(";")
                   let parts = line.Split('|')
                   let key = parts[0]
                   let value = parts.Length > 1 ? parts[1] : null
                   select new KeyValuePair<string, string>(key, value);
        }
    }
}