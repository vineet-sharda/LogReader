using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_LogReader
{
    public class LogGroup
    {
        public LogGroup()
        {
            this.logEntries = new List<LogEntryCollection>();
        }

        public string Message { get; set; }

        private List<LogEntryCollection> logEntries;

        public List<LogEntryCollection> LogEntries
        {
            get { return logEntries; }
            set { logEntries = value; }
        }

        public void Add(string log, string fileId, int lineNumber, DateTime timestamp)
        {
            bool found = false;
            foreach (LogEntryCollection logEntry in this.logEntries)
            {
                if (logEntry.Message == log)
                {
                    logEntry.Add(new LogEntry()
                    {
                        FileId = fileId,
                        LineNumber = lineNumber,
                        TimeStamp = timestamp
                    });
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                LogEntryCollection logEntry = new LogEntryCollection() { Message = log };
                logEntry.Add(new LogEntry()
                {
                    FileId = fileId,
                    LineNumber = lineNumber,
                    TimeStamp = timestamp
                });
                this.logEntries.Add(logEntry);
            }
        }

    }

}
