namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            string[] files = Array.Empty<string>();

            Console.WriteLine("This application will generate C# classes based on csv files");
            Console.WriteLine("Please enter path to your csv files:");

            var path = Console.ReadLine();
            path = path.EndsWith('\\') ? path : path + "\\";

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

            string ProjectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            Console.WriteLine("Generation started");

            foreach (string file in files)
            {
                var gClass = new ClassGenerator(ProjectName, file);
                var gMap = new MapGenerator(ProjectName, file);

                var NewFileName = file.Split('\\').LastOrDefault().Split('.').FirstOrDefault();

                var GeneratedClass = gClass.GenerateClassCode();
                var GeneratedMap = gMap.GenerateMapCode();

                using (FileStream fstream = new FileStream($"{path}{NewFileName + ".cs"}", FileMode.Create))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(GeneratedClass);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine($"Generated class: {path}{NewFileName}");
                }

                using (FileStream fstream = new FileStream($"{path}{NewFileName + "Map.cs"}", FileMode.Create))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(GeneratedMap);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine($"Generated map: {path}{NewFileName}");
                }
            }
        }
    }
}
