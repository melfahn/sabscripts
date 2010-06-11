using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace PreQueue
{
    public class Logger
    {
        private readonly string _logPath;
        private readonly int _maxLogDays;

        public Logger()
        {
            _maxLogDays = Convert.ToInt32(ConfigurationManager.AppSettings["deleteLogs"] ?? "0");

            var assembly = Assembly.GetExecutingAssembly();
            _logPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "log");

            if (!Directory.Exists(_logPath))
                Directory.CreateDirectory(_logPath);
        }

        public void Log(string message)
        {
            try
            {
                string fileName = string.Format("{0:yyyy-MM-dd}.txt", DateTime.Now);
                using (StreamWriter sw = File.AppendText(Path.Combine(_logPath, fileName)))
                {
                    sw.WriteLine(String.Format("[{0:HH-mm-ss}] {1}", DateTime.Now, message));
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