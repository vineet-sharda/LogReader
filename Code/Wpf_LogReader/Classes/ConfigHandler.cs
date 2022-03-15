using System.Configuration;
using Wpf_LogReader.ConfigClasses;

namespace Wpf_LogReader
{
    public static class ConfigHandler
    {
        static ConfigHandler()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            sectionGroup = config.GetSectionGroup("reader") as ReaderSectionGroup;
            sectionGroup = sectionGroup == null ? new ReaderSectionGroup() : sectionGroup;
        }

        private static ReaderSectionGroup sectionGroup;
        public static ReaderSectionGroup ReaderSettings
        {
            get { return sectionGroup; }
        }

    }
}
