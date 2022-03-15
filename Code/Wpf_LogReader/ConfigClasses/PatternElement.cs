using System.Configuration;

namespace Wpf_LogReader.ConfigClasses
{
    public class PatternElement : ConfigurationElement
    {
        public PatternElement(string name, string match, int logStartFrom)
        {
            Name = name;
            Match = match;
            LogStartFrom = logStartFrom;
        }

        // Default constructor, will use default values as defined
        // below.
        public PatternElement()
        {
        }

        // Constructor allowing name to be specified, will take the
        // default values for match and logStartFrom
        public PatternElement(string elementName)
        {
            Name = elementName;
        }

        [ConfigurationProperty("name",
            DefaultValue = "PatternName",
            IsRequired = true,
            IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("match",
            DefaultValue = "^[A-Za-z0-9]{8}-([A-Za-z0-9]{4}-){3}[A-Za-z0-9]{12} [0-9]{4}(-[0-9]{2}){2} [0-9]{2}(:[0-9]{2}){2}",
            IsRequired = true)]
        public string Match
        {
            get
            {
                return (string)this["match"];
            }
            set
            {
                this["match"] = value;
            }
        }

        [ConfigurationProperty("logStartFrom",
            DefaultValue = 0,
            IsRequired = false)]
        [IntegerValidator(MinValue = 0,
            MaxValue = 500, ExcludeRange = false)]
        public int LogStartFrom
        {
            get
            {
                return (int)this["logStartFrom"];
            }
            set
            {
                this["logStartFrom"] = value;
            }
        }

        protected override void DeserializeElement(
           System.Xml.XmlReader reader,
            bool serializeCollectionKey)
        {
            base.DeserializeElement(reader,
                serializeCollectionKey);
            // You can your custom processing code here.
        }

        protected override bool SerializeElement(
            System.Xml.XmlWriter writer,
            bool serializeCollectionKey)
        {
            bool ret = base.SerializeElement(writer,
                serializeCollectionKey);
            // You can enter your custom processing code here.
            return ret;
        }

        protected override bool IsModified()
        {
            bool ret = base.IsModified();
            // You can enter your custom processing code here.
            return ret;
        }
    }
}