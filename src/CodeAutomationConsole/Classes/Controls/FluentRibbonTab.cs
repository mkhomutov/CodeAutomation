namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FluentRibbonTab : Fluent
    {
        public FluentRibbonTab() { }

        public List<FluentRibbonTabItem> TabItems { get; set; }

        public XElement GetXml(string project)
        {
            var xml = new XElement(Ns() + "Ribbon.Tabs",
                TabItems.Select(tabItem => tabItem.GetXml(project)));

            return xml;
        }
    }
}
