using System.Configuration;

namespace Wpf_LogReader.ConfigClasses
{
    public class LogTextSection : ConfigurationSection
    {
        public LogTextSection(int length)
        {
            MaxLength = length;
        }

        // Default constructor, will use default values as defined
        // below.
        public LogTextSection()
        {
        }


        [ConfigurationProperty("maxLength",
            DefaultValue = 100,
            IsRequired = true)]
        public int MaxLength
        {
            get
            {
                return (int)this["maxLength"];
            }
            set
            {
                this["maxLength"] = value;
            }
        }

        protected override void DeserializeSection(
            System.Xml.XmlReader reader)
        {
            base.DeserializeSection(reader);
            // You can add custom processing code here.
        }

        protected override string SerializeSection(
            ConfigurationElement parentElement,
            string name, ConfigurationSaveMode saveMode)
        {
            string s =
                base.SerializeSection(parentElement,
                name, saveMode);
            // You can add custom processing code here.
            return s;
        }
    }
}