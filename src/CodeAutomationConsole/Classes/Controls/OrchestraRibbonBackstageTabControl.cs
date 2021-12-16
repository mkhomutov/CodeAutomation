namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class OrchestraRibbonBackstageTabControl : Orchestra
    {
        public OrchestraRibbonBackstageTabControl() { }

        public string GridRow { get; set; }

        public OrchestraBackstageTabItem OrchestraBackstageTabItem { get; set; }

        public XElement GetXml(string project)
        {
            var gridRow = GridRow is null ? null : new XAttribute("Grid.Row", GridRow);

            var orchestraBackstageTabItem = OrchestraBackstageTabItem is null ? null : OrchestraBackstageTabItem.GetXml(project);

            var xml = new XElement(Ns() + "RibbonBackstageTabControl",
                gridRow,
                orchestraBackstageTabItem);

            return xml;
        }
    }
}
