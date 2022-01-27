namespace CodeAutomationConsole.Tests
{
    using System.Reflection;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Text;

    [TestFixture]
    public class ClassGenerationTest
    {
        private readonly Assembly _assembly;

        private readonly string _classesPrefix;
        private readonly string _csvPrefix;

        public ClassGenerationTest()
        {
            _assembly = Assembly.GetExecutingAssembly();

            _classesPrefix = $"{_assembly.GetName().Name}.Classes.";
            _csvPrefix = $"{_assembly.GetName().Name}.Csv.";
        }

        //[TestCase("SacramentocrimeJanuary2006")]
        //[TestCase("Sacramentorealestatetransactions")]
        //[TestCase("SalesJan2009")]
        //[TestCase("TechCrunchcontinentalUSA")]
        //[TestCase("MachineSiteSetups")]
        //public void CompareClasses(string name)
        //{
        //    var config = new LoadExtendedConfiguration(@"..\..\..\Yaml\CodeAutomation.yml");
        //    var nameSpace = config.NameSpace;
        //    var csvSettings = config.GetCsv(name);

        //    var className = csvSettings?.ClassName ?? name;
        //    string classResource = _classesPrefix + className + ".cs";

        //    var tempCsvFilenme = GetTemporaryCsv(name);

        //    var gClass = csvSettings is null ? new ClassGenerator(nameSpace, tempCsvFilenme) : new ClassGenerator(nameSpace, tempCsvFilenme, csvSettings);

        //    File.Delete(tempCsvFilenme);

        //    var generatedClass = gClass.GenerateClassCode();

        //    var currentClass = LoadResource(classResource);

        //    AssertClass(name, currentClass, generatedClass);
        //}

        private void AssertClass(string className, string currentClass, string genereatedCurrentClass)
        {
            var echo = new StringBuilder();
            var lineNumber = 0;

            using (var classReader = new StringReader(currentClass))
            using (var generatedClassReader = new StringReader(genereatedCurrentClass))
            {
                string line;
                while ((line = classReader.ReadLine()) is not null)
                {
                    lineNumber++;
                    var generatedLine = generatedClassReader.ReadLine();

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
            Console.WriteLine(tempFile);
            return tempFile;
        }
    }
}
