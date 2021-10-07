namespace CodeAutomationConsole.Test
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
        private readonly string _projectName = "CodeAutomationConsole";

        public ClassGenerationTest()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _classesPrefix = $"{_assembly.GetName().Name}.Classes.";
            _csvPrefix = @"..\..\..\Csv\";
        }

        [TestCase("SacramentocrimeJanuary2006")]
        [TestCase("Sacramentorealestatetransactions")]
        [TestCase("SalesJan2009")]
        [TestCase("TechCrunchcontinentalUSA")]
        public void CompareClasses(string name)
        {
            string csvResourceFilename = _csvPrefix + name + ".csv";
            string classResourceFilename = _classesPrefix + name + ".cs";

            try
            {
                Assert.IsTrue(File.Exists(csvResourceFilename));

                var gClass = new ClassGenerator(_projectName, csvResourceFilename);

                var generatedClass = gClass.GenerateClassCode();

                var currentClass = LoadResource(classResourceFilename);

                AssertClass(name, currentClass, generatedClass);
            }
            catch (AssertionException ex)
            {
                Console.WriteLine($"Csv file fore test {name} doesn't exist. {ex.Message}");
            }
        }

        private void AssertClass(string className, string currentClass, string genereatedCurrentClass)
        {
            var echo = new StringBuilder();
            var lineNumber = 0;

            try
            {
                using (var classReader = new StringReader(currentClass))
                using (var generatedClassReader = new StringReader(genereatedCurrentClass))
                {
                    string line;
                    while ((line = classReader.ReadLine()) is not null)
                    {
                        lineNumber++;
                        var generatedLine = generatedClassReader.ReadLine();

                        Assert.AreEqual(line, generatedLine);
                    }
                }
            }
            catch (AssertionException ex)
            {
                echo.AppendLine($"\nError comparing class {className}");
                echo.AppendLine($"   line: {lineNumber}\n");
                echo.AppendLine(ex.Message);

                Console.WriteLine(echo.ToString());

                throw;
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
    }
}
