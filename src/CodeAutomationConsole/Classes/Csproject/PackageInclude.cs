namespace CodeAutomationConsole
{
    using System.Xml.Serialization;

    public class PackageInclude
    {
        public PackageInclude() : this("PackageName", "1.0")
        {

        }

        public PackageInclude(string package, string version)
        {
            Package = package;
            Version = version;
        }

        [XmlAttribute(AttributeName = "Include")]
        public string Package { get; set; }

        [XmlAttribute]
        public string Version { get; set; }
    }
}
