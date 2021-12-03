namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProjectView
    {
        public ProjectView(string name) : this()
        {
            Name = name;
        }

        public ProjectView()
        {
            Ribbon = new FluentRibbon();
        }

        public string Name { get; set; }

        public FluentRibbon Ribbon { get; set; }
    }
}
