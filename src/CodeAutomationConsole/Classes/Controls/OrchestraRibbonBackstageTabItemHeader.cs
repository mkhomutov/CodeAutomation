namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class OrchestraRibbonBackstageTabItemHeader : Orchestra
    {
        public OrchestraRibbonBackstageTabItemHeader() { }

        public string GridRow { get; set; }

        public string HeaderText { get; set; }

        public string HeaderTextStyleKey { get; set; }

        public string Icon { get; set; }

        public XElement GetXml(string project)
        {
            var gridRow = GridRow is null ? null : new XAttribute("Grid.Row", GridRow);

            var header = HeaderText is null ? null : new XAttribute("HeaderText", HeaderText);

            var headerTextStyleKey = HeaderText is null ? null : new XAttribute("HeaderTextStyleKey", HeaderTextStyleKey);

            var icon = Icon is null ? null : new XAttribute("Icon", $"/Resources/Images/{Icon}");


            var xml = new XElement(Ns() + "RibbonBackstageTabItemHeader",
                gridRow,
                header,
                headerTextStyleKey,
                icon);

            return xml;
        }

    }
}
