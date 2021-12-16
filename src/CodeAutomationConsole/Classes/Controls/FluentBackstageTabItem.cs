namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class FluentBackstageTabItem : Fluent
    {
        public FluentBackstageTabItem() { }

        public string Header { get; set; }

        public Grid Grid { get; set; }

        public XElement GetXml(string project)
        {
            var grid = Grid?.GetXml(project);

            var xml = new XElement(Ns() + "BackstageTabItem",
                new XAttribute("Header", Header),
                grid);

            return xml;
        }
    }

}
