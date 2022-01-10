namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileConstants
    {
        public FileConstants(string ns, string projectPath, List<CsvListMember>csv)
        {
            var scopeNames = csv.Select(x => $"public const string {x.ClassName} = \"{x.ClassName}\";").
                ToArray().
                JoinWithTabs(1);
            var fileNames = csv.Select(x => $"public const string {x.Name} = \"{x.Name}.csv\";").
                ToArray().
                JoinWithTabs(1);

            ProjectPath = projectPath;

            Content = @$"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {ns};

internal static class ScopeNames
{{
    {scopeNames}
}}

internal static class FileNames
{{
    {fileNames}
}}
";
        }

        public string Content { get; }

        public string ProjectPath { get; set; }

        public void Save()
        {
            var file = Path.Combine(ProjectPath, "Constants.cs");

            Content.SaveToFile(file);
        }
    }
}
