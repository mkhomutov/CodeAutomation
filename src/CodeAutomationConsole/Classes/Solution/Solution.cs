namespace CodeAutomationConsole
{
    using System;
    using System.IO;

    public class Solution
    {
        private readonly LoadProjectConfiguration _projectConfiguration;

        public Solution(string path)
        {
            _projectConfiguration = new LoadProjectConfiguration(path);
        }

        public void Generate()
        {

            var path = _projectConfiguration.ImportPath;
            var nameSpace = _projectConfiguration.NameSpace;
            var exportPath = Path.Combine(_projectConfiguration.ExportPath, "src");
            var projectPath = Path.Combine(exportPath, nameSpace);

            Directory.CreateDirectory(Path.Combine(projectPath, "Models\\Maps"));
            Directory.CreateDirectory(Path.Combine(projectPath, "Views"));
            Directory.CreateDirectory(Path.Combine(projectPath, "ViewModels"));

            var files = GetFiles(path);

            Console.WriteLine("Generation started");

            // Generate Classes, Maps

            foreach (var file in files)
            {
                var csvName = Path.GetFileNameWithoutExtension(file);

                var csvSettings = _projectConfiguration.GetCsv(csvName);

                var gClass = csvSettings is null ? new ClassGenerator(nameSpace, file) : new ClassGenerator(nameSpace, file, csvSettings);
                var gMap = csvSettings is null ? new MapGenerator(nameSpace, file) : new MapGenerator(nameSpace, file, csvSettings);

                var generatedClass = gClass.GenerateClassCode();
                var generatedMap = gMap.GenerateMapCode();

                var newFileName = csvSettings?.ClassName ?? csvName;

                generatedClass.SaveToFile(Path.Combine(projectPath, "Models", $"{newFileName}.cs"));
                generatedMap.SaveToFile(Path.Combine(projectPath, "Models", "Maps", $"{newFileName}Map.cs"));
            }

            // Generate csproj
            new Csproj(nameSpace, nameSpace).Content.SaveToFile(Path.Combine(projectPath, $"{nameSpace}.csproj"));

            //Generate Solution
            new Sln(nameSpace).Content.SaveToFile(Path.Combine(exportPath, $"{nameSpace}.sln"));

            // Generate View, ViewModels
            new MainView(nameSpace, projectPath).Save();
            new RibbonView(nameSpace, projectPath).Save();
            new StatusBarView(nameSpace, projectPath).Save();

            //Generate app.xaml, app.xaml.cs
            new App(nameSpace, projectPath).Save();

            //Generate ModuleInitializer.cs
            new ModuleInitializer(nameSpace, projectPath).Save();

            new Resources(projectPath).Save();
            new AssemblyInfo(nameSpace, projectPath).Save();
            new Services(nameSpace, projectPath).Save();
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
