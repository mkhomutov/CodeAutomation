using System;
using System.IO;
using System.Text;
using Scriban;
using Scriban.Runtime;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class AutomationSettings
    {
        public string OutputPath { get; set; }
        public string TemplatesPath { get; set; }
        public object CodeModel { get; set; }
        public DataSource DataSource { get; set; }
        public GitSettings Git { get; set; }
        public ScriptsSettings Script { get; set; }


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
    }

    public class DataItem
    {
        public string Name { get; set; }

        public List<FieldDetails> Columns { get; set; }
    }

    public class DataColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
