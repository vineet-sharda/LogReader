using System.Configuration;

namespace Wpf_LogReader.ConfigClasses
{
    public class ReaderSectionGroup : ConfigurationSectionGroup
    {
        [ConfigurationProperty("logText")]
        public LogTextSection LogText
        {
            get
            {
                LogTextSection logText = (LogTextSection)base.Sections["logText"];
                return logText == null ? new LogTextSection() { MaxLength = 100 } : logText;
            }
        }

        [ConfigurationProperty("patternMatcher", IsRequired = false)]
        public PatternMatcherSection PatternMatcherSettings
        {
            get
            {
                PatternMatcherSection patternMatcherSection = (PatternMatcherSection)base.Sections["patternMatcher"];
                patternMatcherSection = patternMatcherSection == null ? new PatternMatcherSection() : patternMatcherSection;
                return patternMatcherSection;
            }
        }

    }
}
