namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class FluentBackstage : Fluent
    {
        public FluentBackstage() { }

        public FluentBackstageTabControl FluentBackstageTabControl { get; set; }

        public XElement GetXml(string project)
        {
            var xml = new XElement(Ns() + "Backstage", FluentBackstageTabControl.GetXml(project));

            return xml;
        }
    }
}
