namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class FluentMenu : Fluent
    {
        public FluentMenu() { }

        public FluentBackstage FluentBackstage { get; set; }

        public XElement GetXml(string project)
        {
            var xml = FluentBackstage is null ? null : new XElement(Ns() + "Ribbon.Menu", FluentBackstage.GetXml(project));

            return xml;
        }
    }
}
