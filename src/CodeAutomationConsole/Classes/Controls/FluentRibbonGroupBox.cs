namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FluentRibbonGroupBox : Fluent
    {
        public FluentRibbonGroupBox() { }

        public string Header { get; set; }

        public List<FluentRibbonButton> Buttons { get; set; }

        public XElement GetXml()
        {
            var xml = new XElement(Ns() + "RibbonGroupBox",
                new XAttribute("Header", Header),
                Buttons.Select(button => button.GetXml()) );

            return xml;
        }
    }
}
