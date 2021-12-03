namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class FluentRibbonTab
    {
        public FluentRibbonTab(List<FluentRibbonTabItem> tabItems)
        {
            TabItems = tabItems;
        }

        public FluentRibbonTab()
        {
            TabItems = new List<FluentRibbonTabItem>();
        }

        public List<FluentRibbonTabItem> TabItems { get; set; }
    }
}
