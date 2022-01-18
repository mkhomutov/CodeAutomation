namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.Linq;

    // Generate solution and project files
    public class Solution
    {
        public Solution(string path)
        {
            var projectConfiguration = new LoadExtendedConfiguration(path);

            Global.Namespace = projectConfiguration.NameSpace;
            Global.ProjectName = projectConfiguration.NameSpace.Split('.').LastOrDefault();
            Global.Path = Path.Combine(projectConfiguration.ExportPath, "src", projectConfiguration.NameSpace);
            Global.CsvList = projectConfiguration.CsvList;
            Global.Config = projectConfiguration;
            Global.ProjectGuid = Guid.NewGuid().ToString().ToUpper();
        }

        public void Generate()
        {
            Console.WriteLine("Generation started");

            // \Commands
            Commands.Save();                    // ProjectOpenCommandContainer.cs

            // \Data\Models
            Global.Config.CsvList.ForEach(x => x.GenerateClass().SaveToFile(Path.Combine(Global.Path, "Data", "Models", $"{x.ClassName}.cs")));
            Global.Config.CsvList.ForEach(x => x.GenerateMap().SaveToFile(Path.Combine(Global.Path, "Data", "Models", "Maps", $"{x.ClassName}Map.cs")));

            // \Data\ProjectManagement
            ProjectManagement.Save();           // MasterData.cs, Project.cs, ProjectReader.cs

            // \Resources
            Resources.Save();

            // \Properties
            AssemblyInfo.Save();                // AssemblyInfo.cs

            // \Services
            Services.Save();                    // ApplicationInitializationService.cs, \UI\Services\RibbonService.cs

            // \UI
            UiModels.Save();                    // ProjectViewConfiguration.cs
            new MainView().Save();
            new RibbonView().Save();
            new StatusBarView().Save();
            Global.Config.GetProjectView("MainView").Tabs.ForEach(x => new Tabs(x).Save()); // Tabs view and viewmodels


            // Project
            App.Save();                         // App.xaml, App.xaml.cs
            ModuleInitializer.Save();           // ModuleInitializer.cs
            FontAwesome.Save();                 // FontAwesome.cs
            Constants.Save();                   // Constants.cs
            Csproj.Save();                      // ProjectName.csproj
            Sln.Save();                         // SolutionName.sln
            ProjectApplicationInfo.Save();      // ProjectApplicationInfo.cs
        }
    }
}
