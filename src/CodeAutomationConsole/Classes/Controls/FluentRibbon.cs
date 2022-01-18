namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FluentRibbon : Fluent
    {
        public FluentRibbon() { }

        public List<FluentRibbonTab> FluentRibbonTabs { get; set; }

        public XElement GetXml()
        {
            var xName = XName.Get("Name", X().ToString());

            var tabs = FluentRibbonTabs is null ? null : FluentRibbonTabs.Select(tab => tab.GetXml());

            var xml = new XElement(Ns() + "Ribbon",
                new XAttribute(xName, "ribbon"),
                new XAttribute("IsQuickAccessToolBarVisible", "False"),
                new XAttribute("CanCustomizeRibbon", "False"),
                new XAttribute("AutomaticStateManagement", "False"),
                tabs);

            return xml;
        }
    }
}
