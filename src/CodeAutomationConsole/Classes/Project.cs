namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    public class Project
    {
        private readonly LoadProjectConfiguration _projectConfiguration;

        public Project(string path)
        {
            _projectConfiguration = new LoadProjectConfiguration(path);
        }

        public void Generate()
        {

            var path = _projectConfiguration.ImportPath;
            var nameSpace = _projectConfiguration.NameSpace;
            var exportPath = _projectConfiguration.ExportPath;

            Directory.CreateDirectory(Path.Combine(exportPath, "Models\\Maps"));

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

                SaveFile($"{exportPath}\\Models", $"{newFileName}.cs", generatedClass);

                SaveFile($"{exportPath}\\Models\\Maps", $"{newFileName}Map.cs", generatedMap);
            }

            // Generate csproj
            var project = new CsProject(nameSpace, nameSpace);

            var emptyNamespaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var ser = new XmlSerializer(typeof(CsProject));

            var settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;

            using (var stream = new StreamWriter(Path.Combine(exportPath, nameSpace+ ".csproj")))
            using (var writer = XmlWriter.Create(stream, settings))
            {
                ser.Serialize(writer, project, emptyNamespaces);
            }

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
