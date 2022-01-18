namespace CodeAutomationConsole
{
    using System.Linq;
    using System.Xml.Linq;

    public class FluentRibbonButton : Fluent
    {
        public FluentRibbonButton() { }

        public string Header { get; set; }
        public string LargeIcon { get; set; }
        public string Command { get; set; }

        public XElement GetXml()
        {
            var command = Command is null ? null : new XAttribute("Command", $"{{Binding {Command}}}");

            var icon = LargeIcon is null ? null : new XAttribute("LargeIcon", $"{{orctheming:FontImage {{x:Static {Global.ProjectName.ToLower()}:FontAwesome.{LargeIcon}}}}}");

            var xml = new XElement(Ns() + "Button",
                new XAttribute("Header", Header), icon, command);

            return xml;
        }

    }
}
