using System.Configuration;

namespace Wpf_LogReader.ConfigClasses
{
    public class PatternDateTimeStampElement : ConfigurationElement
    {
        public PatternDateTimeStampElement(string match)
        {
            Match = match;
        }

        // Default constructor, will use default values as defined
        // below.
        public PatternDateTimeStampElement()
        {
        }


        [ConfigurationProperty("match",
            DefaultValue = "[0-9]{4}(-[0-9]{2}){2} [0-9]{2}(:[0-9]{2}){2}",
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