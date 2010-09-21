using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SABSync.Services
{
    class DiskController : IDiskController
    {
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        public string[] GetDirectories(string path)
        {
            return Directory.GetDirectories(path);
        }

        public String CreateDirectory(string path)
        {
            return Directory.CreateDirectory(path).FullName;
        }


        public string CleanPath(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path can not be null or empty");

            return path.ToLower().Trim('/', '\\', ' ');
        }

    }
}
