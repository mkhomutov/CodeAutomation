namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class Label : Default
    {
        public Label() { }

        public string GridRow { get; set; }

        public string Content { get; set; }

        public List<string> Style { get; set; }

        public XElement GetXml(string project)
        {
            var gridRow = GridRow is null ? null : new XAttribute("Grid.Row", GridRow);

            var content = Content is null ? null : new XAttribute("Content", Content);

            var style = Style is null ? null : new XAttribute("Style", $"{{{Style.Aggregate((x, y) => $"{x} {y}")}}}");

            var xml = new XElement(Ns() + "Label", gridRow, content, style);

            return xml;
        }

    }
}
