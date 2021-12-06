namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ModuleInitializer
    {
        public ModuleInitializer(string ns, string projectPath)
        {
            ProjectPath = projectPath;

            Content = @$"using Catel.IoC;
using {ns}.Services;
using Orchestra.Services;

public static class ModuleInitializer
{{
    public static void Initialize()
    {{
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IRibbonService, RibbonService>();
        serviceLocator.RegisterType<IApplicationInitializationService, ApplicationInitializationService>();
    }}
}}";
        }

        public string ProjectPath { get; set; }

        public string Content { get; set; }

        public void Save()
        {
            var fileName = Path.Combine(ProjectPath, "ModuleInitializer.cs");
            Content.SaveToFile(fileName);
        }
    }
}
