namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class FluentRibbonTabItem
    {
        public FluentRibbonTabItem(string header, List<FluentRibbonGroupBox> groupBoxes) : this(header)
        {
            GroupBoxes = groupBoxes;
        }

        public FluentRibbonTabItem(string header) : this()
        {
            Header = header;
        }

        public FluentRibbonTabItem()
        {
            GroupBoxes = new List<FluentRibbonGroupBox>();
        }

        public string Header { get; set; }

        public List<FluentRibbonGroupBox> GroupBoxes { get; set; }
    }
}
