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

                SaveFile($"{projectPath}\\Models", $"{newFileName}.cs", generatedClass);

                SaveFile($"{projectPath}\\Models\\Maps", $"{newFileName}Map.cs", generatedMap);
            }

            // Generate csproj
            var project = new Csproj(nameSpace, nameSpace);

            SaveFile($"{projectPath}", nameSpace + ".csproj", project.Content);

            //Generate Solution
            var solution = new Sln(nameSpace);
            SaveFile($"{exportPath}", nameSpace + ".sln", solution.Content);

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

        private static void SaveFile(string directory, string file, string generatedClass)
        {
            var newFile = Path.Combine(directory, file);

            using (var fstream = new FileStream(newFile, FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(generatedClass);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine($"Generated: {newFile}");
            }
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
