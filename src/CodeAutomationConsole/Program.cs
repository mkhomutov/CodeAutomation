namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            string configurationPath = @"..\..\..\..\src\CodeAutomationConsole\CodeAutomation.yml";

            var config = new YamlLoad(configurationPath);

            Console.WriteLine("This application will generate C# classes based on csv files");

            var path        = config.ImportPath;
            var nameSpace   = config.NameSpace;
            var exportPath  = config.ExportPath;

            if (!Directory.Exists(exportPath)) { Directory.CreateDirectory(exportPath); }

            var files = GetFiles(path);

            Console.WriteLine("Generation started");

            foreach (var file in files)
            {
                var fileName = file.Split('\\').LastOrDefault().Split('.').FirstOrDefault();

                var csvSettings = config.GetCsv(fileName);

                var gClass = csvSettings is null ? new ClassGenerator(nameSpace, file) : new ClassGenerator(nameSpace, file, csvSettings);
                var gMap = csvSettings is null ? new MapGenerator(nameSpace, file) : new MapGenerator(nameSpace, file, csvSettings);

                var generatedClass = gClass.GenerateClassCode();
                var generatedMap = gMap.GenerateMapCode();

                SaveFile(exportPath, fileName + ".cs", generatedClass);

                SaveFile(exportPath, fileName + "Map.cs", generatedMap);
            }
        }

        private static void SaveFile(string path, string newFileName, string generatedClass)
        {
            using (var fstream = new FileStream($"{path}{newFileName}", FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(generatedClass);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine($"Generated class: {path}{newFileName}");
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
