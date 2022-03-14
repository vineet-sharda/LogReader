using System.Configuration;

namespace Wpf_LogReader.ConfigClasses
{
    public class PatternMatcherSection : ConfigurationSection
    {
        //[ConfigurationProperty("name",
        //    DefaultValue = "MyFavorites",
        //    IsRequired = true,
        //    IsKey = false)]
        //[StringValidator(InvalidCharacters =
        //    " ~!@#$%^&*()[]{}/;'\"|\\",
        //    MinLength = 1, MaxLength = 60)]
        //public string Name
        //{

        //    get
        //    {
        //        return (string)this["name"];
        //    }
        //    set
        //    {
        //        this["name"] = value;
        //    }
        //}

        // Declare an element (not in a collection) of the type
        // UrlConfigElement. In the configuration
        // file it corresponds to <simple .... />.
        [ConfigurationProperty("dateTimeStampPattern")]
        public PatternDateTimeStampElement DateTimeStampPattern
        {
            get
            {
                PatternDateTimeStampElement dateTimeStampPattern = (PatternDateTimeStampElement)base["dateTimeStampPattern"];
                return dateTimeStampPattern == null ? new PatternDateTimeStampElement() { Match = "[0-9]{4}(-[0-9]{2}){2} [0-9]{2}(:[0-9]{2}){2}" } : dateTimeStampPattern;
            }
        }

        [ConfigurationProperty("groupLogSplitter")]
        public PatternGroupLogSplitterElement GroupLogSplitter
        {
            get
            {
                PatternGroupLogSplitterElement groupLogSplitter = (PatternGroupLogSplitterElement)base["groupLogSplitter"];
                return groupLogSplitter == null ? new PatternGroupLogSplitterElement() { Splitter = " - " } : groupLogSplitter;
            }
        }

        // Declare a collection element represented 
        // in the configuration file by the sub-section
        // <urls> <add .../> </urls> 
        // Note: the "IsDefaultCollection = false" 
        // instructs the .NET Framework to build a nested 
        // section like <urls> ...</urls>.
        //private PatternCollection patternCollection;
        [ConfigurationProperty("patterns",
            IsDefaultCollection = false)]
        public PatternCollection Patterns
        {
            get
            {
                PatternCollection patternCollection = (PatternCollection)base["patterns"];
                if (patternCollection.Count == 0)
                {
                    patternCollection.Add(
                        new PatternElement()
                        {
                            Name = "DateTime",
                            Match = "^[0-9]{4}(-[0-9]{2}){2} [0-9]{2}(:[0-9]{2}){2},[0-9]{3}",
                            LogStartFrom = 24
                        });
                    patternCollection.Add(
                        new PatternElement()
                        {
                            Name = "Guid_DateTime",
                            Match = "^[A-Za-z0-9]{8}-([A-Za-z0-9]{4}-){3}[A-Za-z0-9]{12} [0-9]{4}(-[0-9]{2}){2} [0-9]{2}(:[0-9]{2}){2},[0-9]{3}",
                            LogStartFrom = 61
                        });
                }
                return patternCollection;
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
