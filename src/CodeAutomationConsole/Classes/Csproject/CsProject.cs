namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    [XmlRoot("Project")]
    public class CsProject
    {
        public CsProject() : this("", "")
        {

        }

        public CsProject(string name, string root)
        {
            var include = new Dictionary<string, string>();
            include.Add("Orc.CommandLine", "4.1.1");
            include.Add("Orc.Csv", "4.3.2");
            include.Add("Spectre.Console", "0.41.0");
            include.Add("YamlDotNet", "11.2.1");

            var update = new Dictionary<string, string>();
            update.Add("NETStandard.Library", "2.0.3");

            var packages = new Dictionary<string, Dictionary<string, string>>();
            packages.Add("Include", include);
            packages.Add("Update", update);

            PropertyGroup = new PropertyGroup(name, root);

            ItemGroup = packages.Select(x => new ItemGroup(x.Key, x.Value)).ToArray();

        }

        public PropertyGroup PropertyGroup { get; set; }

        [XmlElement]
        public ItemGroup[] ItemGroup { get; set; }
    }
}
