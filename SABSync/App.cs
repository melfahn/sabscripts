using System;
using System.IO;
using System.Reflection;

namespace SABSync
{
    public class App
    {
        private static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
        private static readonly DateTime LastWriteTime = File.GetLastWriteTime(Assembly.Location);

        public static string ExecutablePath
        {
            get
            {
                var uri = new Uri(Assembly.EscapedCodeBase);
                return Path.GetDirectoryName(uri.LocalPath);
            }
        }

        public static string Name
        {
            get { return Assembly.GetName().Name; }
        }

        public static string Version
        {
            get { return Assembly.GetName().Version.ToString(); }
        }

        public static DateTime BuildDate
        {
            get { return LastWriteTime; }
        }
    }
}