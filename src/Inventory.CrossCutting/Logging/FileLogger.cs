using System;
using System.IO;

namespace Inventory.CrossCutting.Logging
{
    public class FileLogger : ILogger
    {
        private readonly string _path;

        public FileLogger()
        {
            _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs.txt");
        }

        public void Info(string message)
        {
            Write("INFO", message);
        }

        public void Error(string message, Exception ex = null)
        {
            Write("ERROR", message + " " + ex?.Message);
        }

        private void Write(string level, string message)
        {
            File.AppendAllText(_path,
                $"{DateTime.Now} [{level}] {message}{Environment.NewLine}");
        }
    }
}