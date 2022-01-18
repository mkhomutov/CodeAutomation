namespace CodeAutomationConsole
{
    using System.IO;
    using System.Text;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class LoadBaseConfiguration
    {
        private readonly string _yaml;

        private readonly BaseConfiguration _configuration;

        public LoadBaseConfiguration(string path)
        {
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                _yaml = sr.ReadToEnd();
            }

            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
            _configuration = deserializer.Deserialize<BaseConfiguration>(_yaml);
        }

        public BaseConfiguration Config
        {
            get => _configuration;
        }
    }
}
