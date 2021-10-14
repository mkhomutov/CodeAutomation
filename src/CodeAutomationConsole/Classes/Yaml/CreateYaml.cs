namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class CreateYaml
    {
        public CreateYaml(string path)
        {
            var config = new Configuration();

            config.CsvImportPath = path;
            config.CodeExportPath = path + "generated\\";

            config.CsvList = new List<CsvListMember>();

            var files = GetFiles(path);

            foreach (var file in files)
            {
                var fileName = file.Split('\\').LastOrDefault().Split('.').FirstOrDefault();

                var csv = new CsvListMember();

                csv.Name = fileName;
                csv.ClassName = fileName.EndsWith('s') ? fileName.Substring(0, fileName.Length - 1) : fileName;
                csv.Details = new ParseCSV(file).Details;

                config.CsvList.Add(csv);
            }

            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

            Yaml = serializer.Serialize(config);
        }

        public string Yaml { get; }

        public void SaveTo(string path)
        {
            var directory = path.Split('\\').SkipLast(1).Aggregate((x, y) => $"x\\y");

            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }

            using (var fstream = new FileStream(path, FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(Yaml);
                fstream.Write(array, 0, array.Length);
            }
        }

        private static string[] GetFiles(string path)
        {
            string[] files = { };

            try
            {
                files = Directory.GetFiles(path, "*.csv");
            }
            catch { }

            return files;
        }

    }
}
