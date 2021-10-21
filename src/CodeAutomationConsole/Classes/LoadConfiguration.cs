namespace CodeAutomationConsole
{
    using System.IO;
    using System.Text;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class LoadConfiguration
    {
        private readonly string _yaml;

        private readonly Configuration _configuration;

        public LoadConfiguration(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                _yaml = sr.ReadToEnd();
            }

            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            _configuration = deserializer.Deserialize<Configuration>(_yaml);
        }

        public Configuration Config
        {
            get => _configuration;
        }
    }
}
