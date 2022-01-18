namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FluentRibbonTabItem : Fluent
    {
        public FluentRibbonTabItem() { }

        public string Header { get; set; }

        public List<FluentRibbonGroupBox> GroupBoxes { get; set; }

        public XElement GetXml()
        {
            var xml = new XElement(Ns() + "RibbonTabItem",
                new XAttribute("Header", Header),
                GroupBoxes.Select(groupBox => groupBox.GetXml()));

            return xml;
        }
    }
}
