namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class CreateYaml
    {
        public CreateYaml(Configuration config)
        {
            var generationConfig = new ProjectConfiguration();

            generationConfig.CsvImportPath = config.CsvPath;
            generationConfig.CodeExportPath = config.ProjectPath;
            generationConfig.NameSpace = $"SES.Projects.{config.Contractor}.{config.ProjectName}";

            generationConfig.CsvList = new List<CsvListMember>();

            var files = GetFiles(config.CsvPath);

            foreach (var file in files)
            {
                var fileName = file.Split('\\').LastOrDefault().Split('.').FirstOrDefault();

                var csv = new CsvListMember();

                csv.Name = fileName;
                csv.ClassName = fileName.EndsWith('s') ? fileName.Substring(0, fileName.Length - 1) : fileName;
                csv.Details = new ParseCSV(file).Details;

                generationConfig.CsvList.Add(csv);
            }

            var serializer = new SerializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();

            Yaml = serializer.Serialize(generationConfig);
        }

        public string Yaml { get; }

        public void SaveTo(string path)
        {
            var directory = Path.GetDirectoryName(path);

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
