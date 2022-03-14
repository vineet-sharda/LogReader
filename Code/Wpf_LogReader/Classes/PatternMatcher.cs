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
        static PatternMatcherSection configPatternSection;
        static PatternMatcher()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configPatternSection = config.GetSection("patternMatcher") as PatternMatcherSection;
            configPatternSection = configPatternSection == null ? new PatternMatcherSection() : configPatternSection;
        }

        public static void LogStart(string text, ref string dateTimeStamp, ref string logWithIdRemoved)
        {
            for (int i = 0; i < configPatternSection.Patterns.Count; i++)
            {
                PatternElement pattern = configPatternSection.Patterns[i];

                Regex regex = new Regex(pattern.Match);
                if (regex.IsMatch(text))
                {
                    regex = new Regex(configPatternSection.DateTimeStampPattern.Match);
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
            int position = text.IndexOf(configPatternSection.GroupLogSplitter.Splitter);
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
