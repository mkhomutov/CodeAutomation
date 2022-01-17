namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class FluentMenu : Fluent
    {
        public FluentMenu() { }

        public FluentBackstage FluentBackstage { get; set; }

        public XElement GetXml()
        {
            var xml = FluentBackstage is null ? null : new XElement(Ns + "Ribbon.Menu", FluentBackstage.GetXml());

            return xml;
        }
    }
}
