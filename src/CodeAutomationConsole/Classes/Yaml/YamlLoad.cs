namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class YamlLoad
    {
        private readonly string _yaml;

        private readonly Configuration _configuration;

        public YamlLoad(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                _yaml = sr.ReadToEnd();
            }

            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            _configuration = deserializer.Deserialize<Configuration>(_yaml);
        }

        public string ImportPath => _configuration.CsvImportPath;
        public string ExportPath => _configuration.CodeExportPath;
        public string NameSpace => _configuration.NameSpace;

        public CsvListMember GetCsv(string name)
        {
            return _configuration.CsvList.Find(x => string.Equals(x.Name, name));
        }

    }
}
