namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class FluentButton : Fluent
    {
        public FluentButton() { }

        public string Header { get; set; }

        public string Icon { get; set; }

        public string Command { get; set; }

        public XElement GetXml()
        {
            var command = Command is null ? null : new XAttribute("Command", $"{{catel:CommandManagerBinding {Command}}}");

            var icon = Icon is null ? null : new XAttribute("Icon", $"pack://application:,,,/{Global.Namespace};component/Resources/Images/{Icon}");

            var xml = new XElement(Ns() + "Button",
                new XAttribute("Header", Header), icon, command);

            return xml;
        }
    }
}
