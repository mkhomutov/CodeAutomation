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

            string Path = Console.ReadLine();
            Path = Path.EndsWith('\\') ? Path : Path + "\\";
            string[] Files = Directory.GetFiles(Path, "*.csv");

            if (Files.Length == 0)
            {
                Console.WriteLine("No one csv file founded in this catalog");
                System.Environment.Exit(1);
            }

            string ProjectName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;

            Console.WriteLine("\nGeneration started\n");

            foreach (string file in Files)
            {                
                string NewFileName = file.Split('\\').Last().Split('.').First() + ".cs";
                CsvClassGenerator Csv = new CsvClassGenerator(ProjectName, file);
                string GeneratedClass = Csv.GenerateClassCode();
                
                using (FileStream fstream = new FileStream($"{Path}{NewFileName}", FileMode.Create))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(GeneratedClass);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine($"Generated class: {Path}{NewFileName}"); 
                }
            }    
        }
    }
}
