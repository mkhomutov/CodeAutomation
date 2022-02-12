using Scriban;
using Scriban.Runtime;

namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class LoadExtendedConfiguration
    {
        private readonly ExtendedConfiguration _configuration;

        public LoadExtendedConfiguration(string path)
        {
            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                var yaml = sr.ReadToEnd();

                _configuration = deserializer.Deserialize<ExtendedConfiguration>(yaml);
            }
        }

        public string ImportPath => _configuration.CsvImportPath;
        public string ExportPath => _configuration.CodeExportPath;
        public string NameSpace => _configuration.NameSpace;
        public List<CsvListMember> CsvList => _configuration.CsvList;

        public CsvListMember GetCsv(string name)
        {
            return _configuration.CsvList.Find(x => string.Equals(x.File, name));
        }

        public ProjectView GetProjectView(string name)
        {
            return _configuration.ProjectViews.Find(x => string.Equals(x.Name, name));
        }

    }
}
