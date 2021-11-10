namespace CodeAutomationConsole
{
    using System.Xml.Serialization;

    public class PackageUpdate
    {
        public PackageUpdate() : this("PackageName", "1.0")
        {

        }

        public PackageUpdate(string package, string version)
        {
            Package = package;
            Version = version;
        }

        [XmlAttribute(AttributeName = "Update")]
        public string Package { get; set; }

        [XmlAttribute]
        public string Version { get; set; }
    }
}
