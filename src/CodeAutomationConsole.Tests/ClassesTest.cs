namespace CodeAutomationConsole.Test
{
    using System.Reflection;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    [TestFixture]
    public class GenerationTest
    {
        private readonly Assembly _assembly;
        private readonly string _classesPrefix;
        private readonly string _generatedClassesPrefix = @"..\..\..\..\..\data\Simple\inputData\";

        public GenerationTest()
        {
            _assembly = Assembly.GetExecutingAssembly();
            _classesPrefix = $"{_assembly.GetName().Name}.Classes.";
        }

        [Test]
        public void CompareClasses()
        {
            var classResources = GetAllClassResources();
            foreach (var classResource in classResources)
            {                
                var currentClass = LoadClass(classResource);                 
                var className = classResource.Substring(_classesPrefix.Length);
                var generatedClassFile = _generatedClassesPrefix + className;
                try
                {
                    Assert.IsTrue(File.Exists(generatedClassFile));
                    var generatedCurrentClass = LoadGeneratedClass(generatedClassFile);
                    AssertClass(className, currentClass, generatedCurrentClass);
                }
                catch (AssertionException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private IEnumerable<string> GetAllClassResources()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string[] resourceNames = assembly.GetManifestResourceNames();

            return resourceNames.Where(x => x.StartsWith(_classesPrefix))
                .OrderBy(x => x);
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
                echo.AppendLine();
                echo.AppendLine($"Error executing scenario {className}");
                echo.AppendLine($"   file name: {className}");
                echo.AppendLine($"   line: {lineNumber}");
                echo.AppendLine();
                echo.AppendLine(ex.Message);

                Console.WriteLine(echo.ToString());

                throw;
            }
        }     

        private static string LoadClass(string classResource)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(classResource))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static string LoadGeneratedClass(string file)
        {
            using (StreamReader reader = new StreamReader(file, System.Text.Encoding.Default))
            {
                return reader.ReadToEnd();
            }  
        }
    }
}
