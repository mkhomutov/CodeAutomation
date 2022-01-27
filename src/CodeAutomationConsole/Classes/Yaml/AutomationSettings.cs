using System.IO;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class ValueContainer
    {
        public string Value { get; set; }
        public List<ValueContainer> Nested { get; set; }
    }

    public class AutomationSettings
    {
        public string Contractor { get; set; }
        public string ProjectName { get; set; }
        public string CsvPath { get; set; }
        public string OutputPath { get; set; }
        public string TemplatesPath { get; set; }
        public string NameSpace { get; set; }
        public List<ValueContainer> MultipleValues { get; set; }

        public List<CsvListMember> CsvList { get; set; } = new List<CsvListMember>();

        public static AutomationSettings Load(string path)
        {
            using var sr = new StreamReader(path, Encoding.Default);
            var yaml = sr.ReadToEnd();

            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            var result = deserializer.Deserialize<AutomationSettings>(yaml);

            return result;
        }

        public void Save(string path)
        {
            var serializer = new SerializerBuilder().
                WithNamingConvention(CamelCaseNamingConvention.Instance).
                ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).
                Build();

            var content = serializer.Serialize(this);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, content);
        }

        //public Dictionary<object, object> LoadMetadata(string path)
        //{
        //    using var textReader = new StreamReader(path);
        //    var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

        //    return (Dictionary<object, object>)deserializer.Deserialize(textReader);
        //}
    }

    public class DataSource
    {
        public string DataSourceType { get; set; } // CSV, DataBase, Excel
        public string Culture { get; set; }
        public List<DataItem> Tables { get; set; }

    }

    public class DataItem
    {
        public string Name { get; set; }

        public List<CsvDetails> Columns { get; set; }
    }

    public class DataColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
