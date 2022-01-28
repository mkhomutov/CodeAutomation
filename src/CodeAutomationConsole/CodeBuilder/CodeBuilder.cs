using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeAutomationConsole
{
    public class CodeBuilder
    {
        private readonly AutomationSettings _settings;

        public CodeBuilder(AutomationSettings settings)
        {
            _settings = settings;
        }

        public void Run()
        {
            var solutionTree = new SolutionTree(_settings.TemplatesPath, _settings);

           // UpdateCsvDetails(_settings);

            solutionTree = BuildCode(_settings, solutionTree);

            solutionTree.Save(_settings.OutputPath); // save generated code

            _settings.Save(Path.Combine(_settings.OutputPath, "config.yaml")); // save updated settings
        }

        private void UpdateCsvDetails(AutomationSettings settings)
        {
            //var files = Directory.GetFiles(settings.CsvPath, "*.csv");

            //settings.CsvList.Clear();

            //foreach (var file in files)
            //{
            //    var fileName = Path.GetFileNameWithoutExtension(file);

            //    var csv = new CsvListMember
            //    {
            //        Name = fileName,
            //        ClassName = fileName.EndsWith('s') ? fileName.Substring(0, fileName.Length - 1) : fileName,
            //        Details = new ParseCSV(file).Details
            //    };

            //    settings.CsvList.Add(csv);
            //}
        }

        private SolutionTree BuildCode(AutomationSettings settings, SolutionTree solutionTree)
        {
            solutionTree.TranslateTemplate();

            return solutionTree;
        }
    }
}
