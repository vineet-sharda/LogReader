using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wpf_LogReader
{
    public class FileReader : IFileReader
    {
        public FileReader()
        {
            this.Logs = new LogGroupCollection();
        }

        public FileHandler FileHandler { get; set; }

        public LogGroupCollection Logs { get; set; }

        public int MaxLogLength { get; set; }

        public void LoadLogs()
        {
            int lineNumber = 0;
            foreach (string line in File.ReadLines(this.FileHandler.Id))
            {
                lineNumber++;

                string log = "";
                string dateTimeStamp = "";
                PatternMatcher.LogStart(line, ref dateTimeStamp, ref log);
                if (string.IsNullOrWhiteSpace(dateTimeStamp) || string.IsNullOrWhiteSpace(log)) continue;

                string[] grouplog = PatternMatcher.SplitGroupAndEntry(log);
                LogGroup group = this.GetGroup(grouplog[0]);

                string logText = grouplog[1];
                if (logText == "") continue;

                logText = logText.Length > this.MaxLogLength ? logText.Substring(0, this.MaxLogLength) : logText;
                group.Add(logText.Trim(), this.FileHandler.Id, lineNumber, DateTime.Parse(dateTimeStamp));
            }
        }

        public string GetFullLog(string filePath, int lineNumber)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                for (int i = 1; i < lineNumber; i++)
                {
                    streamReader.ReadLine();
                }
                StringBuilder sb = new StringBuilder(streamReader.ReadLine());
                sb.AppendLine();

                while (true)
                {
                    string line = streamReader.ReadLine();
                    if (line == null) break;

                    string log = "";
                    string dateTimeStamp = "";
                    PatternMatcher.LogStart(line, ref dateTimeStamp, ref log);
                    if (!string.IsNullOrWhiteSpace(dateTimeStamp)) break;

                    sb.AppendLine(line);
                }

                return sb.ToString();
            }
        }

        private LogGroup GetGroup(string groupMessage)
        {
            LogGroup? group = this.Logs.FirstOrDefault(groupMessage);
            if (group == null)
            {
                group = new LogGroup() { Message = groupMessage };
                this.Logs.Add(group);
            }
            return group;
        }
    }
}
