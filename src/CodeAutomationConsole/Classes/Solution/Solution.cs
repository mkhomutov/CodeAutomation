namespace CodeAutomationConsole
{
    using System;
    using System.IO;

    // Generate solution and project files
    public class Solution
    {
        private readonly LoadProjectConfiguration _projectConfiguration;

        public Solution(string path)
        {
            _projectConfiguration = new LoadProjectConfiguration(path);
        }

        public LoadProjectConfiguration Config { get => _projectConfiguration; }

        public void Generate()
        {
            var path = Config.ImportPath;
            var nameSpace = Config.NameSpace;
            var exportPath = Path.Combine(Config.ExportPath, "src");
            var projectPath = Path.Combine(exportPath, nameSpace);

            Global.Namespace = nameSpace;
            Global.Path = projectPath;
            Global.CsvList = Config.CsvList;
            Global.Config = Config;

            var files = GetFiles(path);

            Console.WriteLine("Generation started");

            // Generate Classes, Maps

            foreach (var file in files)
            {
                var csvName = Path.GetFileNameWithoutExtension(file);

                var csvSettings = Config.GetCsv(csvName);

                var gClass = csvSettings is null ? new ClassGenerator(nameSpace, file) : new ClassGenerator(nameSpace, file, csvSettings);
                var gMap = csvSettings is null ? new MapGenerator(nameSpace, file) : new MapGenerator(nameSpace, file, csvSettings);

                var generatedClass = gClass.GenerateClassCode();
                var generatedMap = gMap.GenerateMapCode();

                var newFileName = csvSettings?.ClassName ?? csvName;

                generatedClass.SaveToFile(Path.Combine(projectPath,"Data", "Models", $"{newFileName}.cs"));
                generatedMap.SaveToFile(Path.Combine(projectPath,"Data", "Models", "Maps", $"{newFileName}Map.cs"));
            }

            var projectGuid = Guid.NewGuid().ToString().ToUpper();

            // Generate csproj
            new Csproj(nameSpace, nameSpace, projectGuid).Content.SaveToFile(Path.Combine(projectPath, $"{nameSpace}.csproj"));

            //Generate Solution
            new Sln(nameSpace, projectGuid).Content.SaveToFile(Path.Combine(exportPath, $"{nameSpace}.sln"));

            // Generate View, ViewModels
            new MainView(nameSpace, projectPath).Save();
            new RibbonView(nameSpace, projectPath, Config.GetRibbon("MainView")).Save();
            new StatusBarView(nameSpace, projectPath).Save();

            //Generate app.xaml, app.xaml.cs
            new App(nameSpace, projectPath).Save();

            //Generate ModuleInitializer.cs
            new ModuleInitializer(nameSpace, projectPath).Save();

            new Resources(projectPath).Save();

            new AssemblyInfo(nameSpace, projectPath).Save();

            new Services(nameSpace, projectPath, Config.CsvList).Save();

            new FontAwesome(nameSpace).Content.AddCopyright("FontAwesome.cs").SaveToFile(Path.Combine(projectPath, "FontAwesome.cs"));

            new FileConstants(nameSpace, projectPath, Config.CsvList).Save();

            new ProjectManagement(nameSpace, projectPath, Config.CsvList).Save();

            new Commands(nameSpace, projectPath).Save();

            UiModels.Save();
            ProjectApplicationInfo.Save();

            Config.CsvList.ForEach(x => new Tab(x).Save());
        }

        private static string[] GetFiles(string path)
        {
            string[] files = { };

            try
            {
                files = Directory.GetFiles(path, "*.csv");
                if (files.Length == 0)
                {
                    Console.WriteLine("No one csv file founded in this catalog");
                    Environment.Exit(1);
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Seems like this path does not exist");
                Environment.Exit(1);
            }

            return files;
        }

    }
}
