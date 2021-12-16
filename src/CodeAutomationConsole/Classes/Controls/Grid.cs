namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class Grid : Default
    {
        public Grid() { }

        public List<string> Style { get; set; }

        public List<string> RowDefinitions { get; set; }

        public List<string> ColumnDefinitions { get; set; }

        public List<Label> Labels { get; set; }

        public OrchestraRibbonBackstageTabControl OrchestraRibbonBackstageTabControl { get; set; }

        public OrchestraRibbonBackstageTabItemHeader OrchestraRibbonBackstageTabItemHeader { get; set; }

        public XElement GetXml(string project)
        {
            var style = Style is null ? null : new XAttribute("Style", $"{{{Style.Aggregate((x, y) => $"{x} {y}")}}}");

            var rows = RowDefinitions is null ? null : new XElement(Ns() + "Grid.RowDefinitions", RowDefinitions.Select(row => new XElement(Ns() + "RowDefinition", new XAttribute("Height", row))));

            var columns = ColumnDefinitions is null ? null : new XElement(Ns() + "Grid.ColumnDefinitions", ColumnDefinitions.Select(row => new XElement(Ns() + "ColumnDefinition", new XAttribute("Width", row))));

            var labels = Labels is null ? null : Labels.Select(label => label.GetXml(project));

            var orchestraRibbonBackstageTabControl = OrchestraRibbonBackstageTabControl?.GetXml(project);

            var orchestraRibbonBackstageTabItemHeader = OrchestraRibbonBackstageTabItemHeader?.GetXml(project);

            var xml = new XElement(Ns() + "Grid",
                style,
                rows,
                columns,
                labels,
                orchestraRibbonBackstageTabControl,
                orchestraRibbonBackstageTabItemHeader
                );

            return xml;
        }
    }
}
