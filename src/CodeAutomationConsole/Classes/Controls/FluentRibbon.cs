namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FluentRibbon
    {
        public FluentRibbon(List<FluentRibbonTab> tabs) : this()
        {
            Tabs = tabs;
        }

        public FluentRibbon()
        {
            Tabs = new List<FluentRibbonTab>();
        }

        public List<FluentRibbonTab> Tabs { get; set; }
    }
}
