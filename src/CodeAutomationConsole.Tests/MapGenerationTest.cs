namespace CodeAutomationConsole.Tests
{
    using System.Reflection;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Text;

    [TestFixture]
    public class MapGenerationTest
    {
        private readonly Assembly _assembly;

        private readonly string _mapsPrefix;
        private readonly string _csvPrefix;

        public MapGenerationTest()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _mapsPrefix = $"{_assembly.GetName().Name}.Maps.";
            _csvPrefix = $"{_assembly.GetName().Name}.Csv.";
        }

        //[TestCase("SacramentocrimeJanuary2006")]
        //[TestCase("Sacramentorealestatetransactions")]
        //[TestCase("SalesJan2009")]
        //[TestCase("TechCrunchcontinentalUSA")]
        //[TestCase("MachineSiteSetups")]
        //public void CompareMaps(string name)
        //{
        //    var config = new LoadExtendedConfiguration(@"..\..\..\Yaml\CodeAutomation.yml");
        //    var nameSpace = config.NameSpace;
        //    var csvSettings = config.GetCsv(name);

        //    var className = csvSettings?.ClassName ?? name;
        //    var mapResourceFilename = _mapsPrefix + className + "Map.cs";

        //    var tempCsvFilenme = GetTemporaryCsv(name);

        //    var gMap = csvSettings is null ? new MapGenerator(nameSpace, tempCsvFilenme) : new MapGenerator(nameSpace, tempCsvFilenme, csvSettings);

        //    File.Delete(tempCsvFilenme);

        //    var generatedMap = gMap.GenerateMapCode();

        //    var currentClass = LoadResource(mapResourceFilename);

        //    AssertMap(name, currentClass, generatedMap);
        //}

        private void AssertMap(string className, string currentClass, string genereatedCurrentClass)
        {
            var lineNumber = 0;

            using (var classReader = new StringReader(currentClass))
            using (var generatedMapReader = new StringReader(genereatedCurrentClass))
            {
                string line;
                while ((line = classReader.ReadLine()) is not null)
                {
                    lineNumber++;
                    var generatedLine = generatedMapReader.ReadLine();

                    Assert.AreEqual(line, generatedLine, $"\nline: {lineNumber}\n");
                }
            }
        }

        private static string LoadResource(string classResource)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(classResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private string GetTemporaryCsv(string name)
        {
            var csvResource = _csvPrefix + name + ".csv";

            var csvContent = LoadResource(csvResource);

            var tempFile = Path.GetTempPath() + name + ".csv";

            using (FileStream fstream = new FileStream(tempFile, FileMode.Create))
            {
                byte[] array = Encoding.Default.GetBytes(csvContent);
                fstream.Write(array, 0, array.Length);
            }

            return tempFile;
        }
    }
}
