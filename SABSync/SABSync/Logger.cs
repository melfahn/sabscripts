using System;
using System.Configuration;
using System.IO;

namespace SABSync
{
    public class Logger
    {
        private const string LogFolder = "log";
        private readonly string _logPath;
        private readonly bool _loggingEnabled;
        private readonly int _maxLogDays;

        public Logger()
        {
            _loggingEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["loggingEnabled"] ?? "true");
            if (!_loggingEnabled) return;

            _maxLogDays = Convert.ToInt32(ConfigurationManager.AppSettings["deleteLogs"] ?? "0");

            _logPath = Path.Combine(App.ExecutablePath, LogFolder);

            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);
        }
        
        public void Log()
        {
            Log(string.Empty);
        }

        public void Log(string message)
        {
            if (!_loggingEnabled) return;
            Console.WriteLine(message);
            try
            {
                string fileName = string.Format("{0:MM.dd-HH-mm}.txt", DateTime.Now);
                using (StreamWriter sw = File.AppendText(Path.Combine(_logPath, fileName)))
                {
                    sw.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Log(string message, params object[] args)
        {
            Log(string.Format(message, args));
        }

        public void DeleteLogs()
        {
            if (!_loggingEnabled) return;
            if (_maxLogDays <= 0) return;

            foreach (string fileName in Directory.GetFiles(_logPath, "*.txt"))
            {
                var fileInfo = new FileInfo(fileName);
                TimeSpan age = DateTime.Now - fileInfo.LastWriteTime;
                if (age.Days > _maxLogDays)
                {
                    Log("Deleting Log File: {0}", Path.GetFileName(fileName));
                    fileInfo.Delete();
                }
            }
        }
    }
}