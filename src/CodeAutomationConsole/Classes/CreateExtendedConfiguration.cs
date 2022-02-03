namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class CreateExtendedConfiguration
    {
        public CreateExtendedConfiguration(AutomationSettings initialConfig)
        {
            var extendedConfig = new ExtendedConfiguration(initialConfig);

            var mainView = extendedConfig.GetProjectView("MainView");

            var files = GetFiles(""/*initialConfig.CsvPath*/);

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);

                var csv = new CsvListMember();

                csv.File = fileName;
                csv.ClassName = fileName.EndsWith('s') ? fileName.Substring(0, fileName.Length - 1) : fileName;
                csv.Fields = new ParseCSV(file).Details;

                extendedConfig.CsvList.Add(csv);

            }

            if (mainView.Tabs is null)
            {
                mainView.Tabs = new List<ViewTab>();
                extendedConfig.CsvList.ForEach(x =>
                {
                    var tab = new ViewTab(x.ClassName, x.File);
                    var content = new Content();

                    var dataGrid = new DataGrid(x.Fields);
                    content.DataGrid = dataGrid;

                    tab.Content.Add(content);
                    mainView.Tabs.Add(tab);
                });
            }

            var serializer = new SerializerBuilder().
                WithNamingConvention(CamelCaseNamingConvention.Instance).
                ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).
                Build();

            Content = serializer.Serialize(extendedConfig);
        }

        public string Content { get; }

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
