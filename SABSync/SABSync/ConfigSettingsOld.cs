using System;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;

namespace SABSync
{
    public static class ConfigSettingsOld
    {
        private static readonly Configuration Config = ConfigurationManager.OpenExeConfiguration(@"SABSync.exe");

        internal static string TvRootPath
        {
            get { return GetConfigValue("tvRoot", "", true); }
            set { UpdateValue("tvRoot", value); }
        }

        internal static string TvTemplate
        {
            get { return GetConfigValue("tvTemplate", "", true); }
            set { UpdateValue("tvTemplate", value); }
        }

        internal static string TvDailyTemplate
        {
            get { return GetConfigValue("tvDailyTemplate", "", true); }
            set { UpdateValue("tvDailyTemplate", value); }
        }

        internal static string VideoExt
        {
            get { return GetConfigValue("videoExt", "", true); }
            set { UpdateValue("videoExt", value); }
        }

        internal static string IgnoreSeasons
        {
            get { return GetConfigValue("ignoreSeasons", "", true).Replace(";", Environment.NewLine); }
            set { UpdateValue("ignoreSeasons", value.Replace(Environment.NewLine, ";")); }
        }

        internal static string NzbDir
        {
            get { return GetConfigValue("nzbDir", "", true); }
            set { UpdateValue("nzbDir", value); }
        }

        internal static string SabnzbdInfo
        {
            get { return GetConfigValue("sabnzbdInfo", "", true); }
            set { UpdateValue("sabnzbdInfo", value); }
        }

        internal static string Username
        {
            get { return GetConfigValue("username", "", true); }
            set { UpdateValue("username", value); }
        }

        internal static string Password
        {
            get { return GetConfigValue("password", "", true); }
            set { UpdateValue("password", value); }
        }

        internal static string ApiKey
        {
            get { return GetConfigValue("apiKey", "", true); }
            set { UpdateValue("apiKey", value); }
        }

        internal static string Priority
        {
            get { return GetConfigValue("priority", "", true); }
            set { UpdateValue("priority", value); }
        }

        internal static string RssConfig
        {
            get { return GetConfigValue("rss", "", true); }
            set { UpdateValue("rss", value); }
        }

        internal static string AliasConfig
        {
            get { return GetConfigValue("alias", "", true); }
            set { UpdateValue("alias", value); }
        }

        internal static string QualityConfig
        {
            get { return GetConfigValue("quality", "", true); }
            set { UpdateValue("quality", value); }
        }

        internal static string DownloadQuality
        {
            get { return GetConfigValue("downloadQuality", "", true); }
            set { UpdateValue("downloadQuality", value); }
        }

        internal static string SabReplaceChars
        {
            get { return GetConfigValue("SabReplaceChars", "", true); }
            set { UpdateValue("SabReplaceChars", value); }
        }

        internal static string VerboseLogging
        {
            get { return GetConfigValue("verboseLogging", "", true); }
            set { UpdateValue("verboseLogging", value); }
        }

        internal static string DownloadPropers
        {
            get { return GetConfigValue("downloadPropers", "", true); }
            set { UpdateValue("downloadPropers", value); }
        }

        internal static string DeleteLogs
        {
            get { return GetConfigValue("deleteLogs", "", true); }
            set { UpdateValue("deleteLogs", value); }
        }

        internal static string Interval
        {
            get { return GetConfigValue("interval", "", true); }
            set { UpdateValue("interval", value); }
        }

        private static void UpdateValue(string key, object value)
        {
            Config.AppSettings.Settings.Remove(key);
            Config.AppSettings.Settings.Add(key, value.ToString());
            Config.Save();
        }

        private static string GetConfigValue(string key, object defaultValue, bool makePermanent)
        {
            string value;

            if (Config.AppSettings.Settings[key] != null)
            {
                value = Config.AppSettings.Settings[key].Value;
            }
            else
            {
                if (makePermanent)
                {
                    UpdateValue(key, defaultValue.ToString());
                }
                value = defaultValue.ToString();
            }
            return value;
        }
    }
}