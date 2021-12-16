namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FluentRibbon : Fluent
    {
        public FluentRibbon() { }

        public FluentMenu FluentMenu { get; set; }

        public List<FluentRibbonTab> Tabs { get; set; }

        public XElement GetXml(string project)
        {
            var xName = XName.Get("Name", X().ToString());

            var menu = FluentMenu is null ? null : FluentMenu.GetXml(project);

            var tabs = Tabs is null ? null : Tabs.Select(tab => tab.GetXml(project));

            var xml = new XElement(Ns() + "Ribbon",
                new XAttribute(xName, "ribbon"),
                new XAttribute("IsQuickAccessToolBarVisible", "False"),
                new XAttribute("CanCustomizeRibbon", "False"),
                new XAttribute("AutomaticStateManagement", "False"),
                menu,
                tabs);

            return xml;
        }
    }
}
