namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class BaseConfiguration
    {
        public string Contractor { get; set; }
        public string ProjectName { get; set; }
        public string CsvPath { get; set; }
        public string ProjectPath { get; set; }

        public List<ProjectView> ProjectViews { get; set; }
    }
}
