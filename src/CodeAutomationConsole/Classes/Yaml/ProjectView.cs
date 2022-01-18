namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class ProjectView
    {
        public ProjectView() { }

        public string Name { get; set; }

        public FluentRibbon FluentRibbon { get; set; }

        public List<ViewTab> Tabs { get; set; }
    }
}
