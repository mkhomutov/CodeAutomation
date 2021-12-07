namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class LoadProjectConfiguration
    {
        private readonly ProjectConfiguration _configuration;

        public LoadProjectConfiguration(string path)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                var yaml = sr.ReadToEnd();

                _configuration = deserializer.Deserialize<ProjectConfiguration>(yaml);
            }
        }

        public string ImportPath => _configuration.CsvImportPath;
        public string ExportPath => _configuration.CodeExportPath;
        public string NameSpace => _configuration.NameSpace;

        public CsvListMember GetCsv(string name)
        {
            return _configuration.CsvList.Find(x => string.Equals(x.Name, name));
        }

        public FluentRibbon GetRibbon(string name)
        {
            return _configuration.ProjectViews.Find(x => string.Equals(x.Name, name)).FluentRibbon;
        }

    }
}
