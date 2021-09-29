namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This application will generate C# classes based on csv files");
            Console.WriteLine("Please enter path to your csv files:");

            string path = Console.ReadLine();
            path = path.EndsWith('\\') ? path : path + "\\";
            string[] files = Array.Empty<string>();

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
                string NewFileName = file.Split('\\').Last().Split('.').First() + ".cs";
                CsvClassGenerator Csv = new CsvClassGenerator(ProjectName, file);
                string GeneratedClass = Csv.GenerateClassCode();
                
                using (FileStream fstream = new FileStream($"{path}{NewFileName}", FileMode.Create))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(GeneratedClass);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine($"Generated class: {path}{NewFileName}"); 
                }
            }    
        }
    }
}
