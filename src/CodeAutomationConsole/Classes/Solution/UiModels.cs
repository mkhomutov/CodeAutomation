namespace CodeAutomationConsole
{
    using System.IO;

    public static class UiModels
    {
        private static readonly string ProjectViewConfiguration = $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {Global.Namespace}.UI.Models;

public class ProjectViewConfiguration
{{
    public bool IsOpenProjectBackstageTabItemVisible {{ get; set; }} = true;
    public bool IsOpenProjectViewVisible {{ get; set; }} = true;
    public bool IsSaveProjectBackstageButtonVisible {{ get; set; }} = true;
    public bool IsSaveAsProjectBackstageButtonVisible {{ get; set; }} = true;
    public bool IsCloseProjectBackstageButtonVisible {{ get; set; }} = true;
}}
";

        public static void Save()
        {
            var projectViewConfigurationFile = Path.Combine(Global.Path, "UI", "Models", "ProjectViewConfiguration.cs");

            ProjectViewConfiguration.SaveToFile(projectViewConfigurationFile);
        }
    }
}
