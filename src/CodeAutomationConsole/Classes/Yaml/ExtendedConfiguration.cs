namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class ExtendedConfiguration
    {
        public ExtendedConfiguration() { }

        public ExtendedConfiguration(AutomationSettings initialConfig)
        {
            CsvImportPath = "";//initialConfig.CsvPath;
            CodeExportPath = initialConfig.OutputPath;
            NameSpace = ""; //$"SES.Projects.{initialConfig.Contractor}.{initialConfig.ProjectName}";

            CsvList = new List<CsvListMember>();

            ProjectViews = new List<ProjectView>();
        }
        public string CsvImportPath { get; set; }
        public string CodeExportPath { get; set; }
        public string NameSpace { get; set; }

        public List<CsvListMember> CsvList { get; set; }

        public List<ProjectView> ProjectViews { get; set; }

        public ProjectView GetProjectView(string name)
        {
            var view = ProjectViews.Find(x => x.Name.Equals(name));

            if (view is null)
            {
                view = new ProjectView();
                view.Name = name;
            }

            ProjectViews.Add(view);

            return view;
        }
    }
}
