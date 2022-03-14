using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_LogReader
{
    public class LogGroupCollection
    {
        public LogGroupCollection()
        {
            this.LogGroups = new List<LogGroup>();
            //this.LogGroups.Add(new LogGroup() { Message = ORPHAN_GROUPMSG });
        }
        public List<LogGroup> LogGroups { get; set; }

        public const string ORPHAN_GROUPMSG = "-- ORPHAN --";

        public LogGroup? FirstOrDefault(string message)
        {
            return this.LogGroups.FirstOrDefault(lg => lg.Message == message);
        }
        public void Add(LogGroup logGroup)
        {
            if (!this.Contains(logGroup))
            {
                this.LogGroups.Add(logGroup);
            }
        }
        public void RemoveAll()
        {
            this.LogGroups.RemoveAll(lg => lg.Message != ORPHAN_GROUPMSG);
        }

        private bool Contains(LogGroup logGroup)
        {
            return this.LogGroups.Any(lg => lg.Message == logGroup.Message);
        }
    }
}
