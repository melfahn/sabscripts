using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;

namespace SABSync
{
    public static class ConfigSettings
    {
        internal static string TvRootPath
        {
            get { return GetConfigValue("TvRoot"); }
            set { UpdateValue("TvRoot", value); }
        }

        internal static string TvTemplate
        {
            get { return GetConfigValue("TvTemplate"); }
            set { UpdateValue("TvTemplate", value); }
        }

        internal static string TvDailyTemplate
        {
            get { return GetConfigValue("TvDailyTemplate"); }
            set { UpdateValue("TvDailyTemplate", value); }
        }

        internal static string VideoExt
        {
            get { return GetConfigValue("VideoExt"); }
            set { UpdateValue("VideoExt", value); }
        }

        internal static string IgnoreSeasons
        {
            get { return GetConfigValue("IgnoreSeasons").Replace(";", Environment.NewLine); }
            set { UpdateValue("IgnoreSeasons", value.Replace(Environment.NewLine, ";")); }
        }

        internal static string NzbDir
        {
            get { return GetConfigValue("NzbDir"); }
            set { UpdateValue("NzbDir", value); }
        }

        internal static string SabNzbdInfo
        {
            get { return GetConfigValue("SabNzbdInfo"); }
            set { UpdateValue("SabNzbdInfo", value); }
        }

        internal static string Username
        {
            get { return GetConfigValue("Username"); }
            set { UpdateValue("Username", value); }
        }

        internal static string Password
        {
            get { return GetConfigValue("Password"); }
            set { UpdateValue("Password", value); }
        }

        internal static string ApiKey
        {
            get { return GetConfigValue("ApiKey"); }
            set { UpdateValue("ApiKey", value); }
        }

        internal static string Priority
        {
            get { return GetConfigValue("Priority"); }
            set { UpdateValue("Priority", value); }
        }

        internal static string RssConfig
        {
            get { return GetConfigValue("Rss"); }
            set { UpdateValue("Rss", value); }
        }

        internal static string AliasConfig
        {
            get { return GetConfigValue("Alias"); }
            set { UpdateValue("Alias", value); }
        }

        internal static string QualityConfig
        {
            get { return GetConfigValue("Quality"); }
            set { UpdateValue("Quality", value); }
        }

        internal static string DownloadQuality
        {
            get { return GetConfigValue("DownloadQuality"); }
            set { UpdateValue("DownloadQuality", value); }
        }

        internal static string SabReplaceChars
        {
            get { return GetConfigValue("SabReplaceChars"); }
            set { UpdateValue("SabReplaceChars", value); }
        }

        internal static string VerboseLogging
        {
            get { return GetConfigValue("VerboseLogging"); }
            set { UpdateValue("VerboseLogging", value); }
        }

        internal static string DownloadPropers
        {
            get { return GetConfigValue("DownloadPropers"); }
            set { UpdateValue("DownloadPropers", value); }
        }

        internal static string DeleteLogs
        {
            get { return GetConfigValue("DeleteLogs"); }
            set { UpdateValue("DeleteLogs", value); }
        }

        internal static string Interval
        {
            get { return GetConfigValue("Interval"); }
            set { UpdateValue("Interval", value); }
        }

        internal static string SyncOnStart
        {
            get { return GetConfigValue("SyncOnStart"); }
            set { UpdateValue("SyncOnStart", value); }
        }

        private static void UpdateValue(string element, object value)
        {
            Config config = new Config();
            config.SaveValue(element, value.ToString());
        }

        private static string GetConfigValue(string key)
        {
            Config config = new Config();
            return config.GetValue(key);
        }
    }
}