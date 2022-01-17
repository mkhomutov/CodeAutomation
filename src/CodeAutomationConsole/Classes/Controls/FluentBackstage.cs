namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class FluentBackstage : Fluent
    {
        public FluentBackstage() { }

        public FluentBackstageTabControl FluentBackstageTabControl { get; set; }

        public XElement GetXml()
        {
            var xml = new XElement(Ns + "Backstage", FluentBackstageTabControl.GetXml());

            return xml;
        }
    }
}
