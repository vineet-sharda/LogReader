using System.Configuration;

namespace Wpf_LogReader.ConfigClasses
{
    public class PatternGroupLogSplitterElement : ConfigurationElement
    {
        public PatternGroupLogSplitterElement(string splitter)
        {
            Splitter = splitter;
        }

        // Default constructor, will use default values as defined
        // below.
        public PatternGroupLogSplitterElement()
        {
        }


        [ConfigurationProperty("splitter",
            DefaultValue = " - ",
            IsRequired = true)]
        public string Splitter
        {
            get
            {
                return (string)this["splitter"];
            }
            set
            {
                this["splitter"] = value;
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