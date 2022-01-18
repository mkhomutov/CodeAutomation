namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FluentRibbonTab : Fluent
    {
        public FluentRibbonTab() { }

        public List<FluentRibbonTabItem> FluentRibbonTabItems { get; set; }

        public XElement GetXml()
        {
            var xmlns = XNamespace.Xmlns;
            //XNamespace defaultXmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";

            var xml = new XElement(Ns() + "Ribbon.Tabs",
                new XAttribute("xmlns", X()),
                new XAttribute(xmlns + "fluent", Ns()),
                FluentRibbonTabItems.Select(tabItem => tabItem.GetXml()));

            return xml;
        }
    }
}
