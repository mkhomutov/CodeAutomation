namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AssemblyInfo
    {
        public AssemblyInfo(string ns, string projectPath)
        {
            ProjectPath = projectPath;

            Content = $@"
using System.Reflection;
using System.Runtime.InteropServices;

//[assembly: AssemblyTitle(""{ns}"")]
//[assembly: AssemblyProduct(""{ns}"")]
//[assembly: AssemblyDescription(""{ns}"")]
";

        }

        public string ProjectPath { get; set; }

        public string Content { get; set; }

        public void Save()
        {
            var fileName = Path.Combine(ProjectPath, "Properties", "AssemblyInfo.cs");

            Content.AddCopyright("AssemblyInfo.cs").SaveToFile(fileName);
        }
    }
}
