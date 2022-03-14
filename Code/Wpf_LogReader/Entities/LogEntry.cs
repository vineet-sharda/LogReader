using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_LogReader
{
    public class LogEntry
    {
        public string FileId { get; set; }
        public int LineNumber { get; set; }

        public DateTime TimeStamp { get; set; }

    }
}
