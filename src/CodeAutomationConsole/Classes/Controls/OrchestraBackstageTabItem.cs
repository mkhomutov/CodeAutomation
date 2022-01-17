namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class OrchestraBackstageTabItem : Orchestra
    {
        public OrchestraBackstageTabItem() { }

        public string HeaderText { get; set; }

        public string Icon { get; set; }

        public Grid Grid { get; set; }

        public XElement GetXml()
        {
            var grid = Grid?.GetXml();

            var header = HeaderText is null ? null : new XAttribute("HeaderText", HeaderText);

            var icon = Icon is null ? null : new XAttribute("Icon", $"/Resources/Images/{Icon}");


            var xml = new XElement(Ns + "RibbonBackstageTabItem",
                header,
                icon,
                grid);

            return xml;
        }
    }

}
