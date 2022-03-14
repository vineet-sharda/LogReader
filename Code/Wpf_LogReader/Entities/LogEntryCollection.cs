using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_LogReader
{
    public class LogEntryCollection
    {
        public LogEntryCollection()
        {
            this.LogEntries = new List<LogEntry>();
        }
        public string Message { get; set; }

        public List<LogEntry> LogEntries { get; set; }

        public void Add(LogEntry logEntry)
        {
            this.LogEntries.Add(logEntry);
        }

    }
}
