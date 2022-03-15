using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Wpf_LogReader.ConfigClasses;

namespace Wpf_LogReader
{
    public static class PatternMatcher
    {
        public static void LogStart(string text, ref string dateTimeStamp, ref string logWithIdRemoved)
        {
            PatternCollection patterns = ConfigHandler.ReaderSettings.PatternMatcherSettings.Patterns;
            for (int i = 0; i < patterns.Count; i++)
            {
                PatternElement pattern = patterns[i];

                Regex regex = new Regex(pattern.Match);
                if (regex.IsMatch(text))
                {
                    regex = new Regex(ConfigHandler.ReaderSettings.PatternMatcherSettings.DateTimeStampPattern.Match);
                    dateTimeStamp = regex.Match(text).Value;
                    logWithIdRemoved = text.Substring(pattern.LogStartFrom);
                    return;
                }
            }

            dateTimeStamp = "";
            logWithIdRemoved = "";
        }

        public static string[] SplitGroupAndEntry(string text)
        {
            int position = text.IndexOf(ConfigHandler.ReaderSettings.PatternMatcherSettings.GroupLogSplitter.Splitter);
            if (position < 0)
            {
                return new string[] { text, "" };
            }
            return new string[] {
                text.Substring(0, position),
                text.Substring(position+3)
            };
        }
    }

}
