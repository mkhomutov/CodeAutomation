namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class CreateYaml
    {
        public CreateYaml(Configuration config)
        {
            var generationConfig = new ProjectConfiguration();

            generationConfig.CsvImportPath = config.CsvPath;
            generationConfig.CodeExportPath = config.ProjectPath;
            generationConfig.NameSpace = $"SES.Projects.{config.Contractor}.{config.ProjectName}";

            generationConfig.CsvList = new List<CsvListMember>();

            var files = GetFiles(config.CsvPath);

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                var csv = new CsvListMember();

                csv.Name = fileName;
                csv.ClassName = fileName.EndsWith('s') ? fileName.Substring(0, fileName.Length - 1) : fileName;
                csv.Details = new ParseCSV(file).Details;

                generationConfig.CsvList.Add(csv);
            }

            //generationConfig.ProjectViews = new List<ProjectView>();
            generationConfig.ProjectViews =  config.ProjectViews;

            //var refresh = new FluentRibbonButton("Refresh", "Refresh");
            //var mobile = new FluentRibbonButton("Mobile", "Mobile");

            //var workspace = new FluentRibbonGroupBox("Workspace");
            //workspace.Buttons.Add(refresh);
            //workspace.Buttons.Add(mobile);

            //var view1 = new FluentRibbonTabItem("View1");
            //view1.GroupBoxes.Add(workspace);

            //var tabs = new FluentRibbonTab();
            //tabs.TabItems.Add(view1);


            //var mainView = new ProjectView("MainView");
            //mainView.Ribbon.Tabs.Add(tabs);

            //generationConfig.ProjectViews.Add(mainView);

            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

            Content = serializer.Serialize(generationConfig);
        }

        public string Content { get; }

        private static string[] GetFiles(string path)
        {
            string[] files = { };

            try
            {
                files = Directory.GetFiles(path, "*.csv");
            }
            catch { }

            return files;
        }

    }
}
