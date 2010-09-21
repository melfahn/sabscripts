using System;

namespace SABSync.Services
{
    public interface IDiskController
    {
        bool Exists(string path);
        string[] GetDirectories(string path);
        String CreateDirectory(string path);
        string CleanPath(string path);
    }
}