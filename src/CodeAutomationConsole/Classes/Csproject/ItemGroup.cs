namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    public class ItemGroup
    {
        public ItemGroup() : this("", new Dictionary<string, string>())
        {
        }

        public ItemGroup(string type, Dictionary<string, string> packages)
        {
            if (type.Equals("Include"))
            {
                PackageInclude = packages.Select(x => new PackageInclude(x.Key, x.Value)).ToArray();
            }

            switch (type)
            {
                case "Include":
                    PackageInclude = packages.Select(x => new PackageInclude(x.Key, x.Value)).ToArray();
                    break;
                case "Update":
                    PackageUpdate = packages.Select(x => new PackageUpdate(x.Key, x.Value)).ToArray();
                    break;
            }
        }

        [XmlElement]
        public PackageInclude[] PackageInclude { get; set; }

        [XmlElement]
        public PackageUpdate[] PackageUpdate { get; set; }
    }
}
