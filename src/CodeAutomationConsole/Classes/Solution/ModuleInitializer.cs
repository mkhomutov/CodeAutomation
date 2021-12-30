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
using Catel.MVVM;
using Gum;
using Gum.Drawing;
using Orchestra.Services;
using {ns}.Services;
using {ns}.UI.Services;

namespace {ns};

public static class ModuleInitializer
{{
    public static void Initialize()
    {{
        Figure.IsErasingRequiredOnStylePropertyChanged = true;

        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<ICommandManager, CommandManager>();
        serviceLocator.RegisterType<ApplicationInfo, ProjectApplicationInfo>();
        serviceLocator.RegisterType<IRibbonService, RibbonService>();
        serviceLocator.RegisterType<IApplicationInitializationService, ApplicationInitializationService>();

        // ***** IMPORTANT NOTE *****
        //
        // Only register the shell services in the ModuleInitializer. All other types must be registered
        // in the ApplicationInitializationService
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
