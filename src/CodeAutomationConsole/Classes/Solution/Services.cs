namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Services
    {
        public Services(string ns, string projectPath)
        {
            ProjectPath = projectPath;

            ApplicationInitializationServiceContent = $@"
namespace {ns}.Services
{{
    using Orchestra.Services;

    public class ApplicationInitializationService : ApplicationInitializationServiceBase
    {{
        public override bool ShowSplashScreen => true;

        public override bool ShowShell => true;
    }}
}}
";
            RibbonServiceContent = $@"
namespace {ns}.Services
{{
    using System.Windows;
    using Orchestra.Services;
    using Views;

    public class RibbonService : IRibbonService
    {{
        public FrameworkElement GetRibbon()
        {{
            return new RibbonView();
        }}

        public FrameworkElement GetMainView()
        {{
            return new MainView();
        }}

        public FrameworkElement GetStatusBar()
        {{
            return new StatusBarView();
        }}
    }}
}}
";
        }

        public string ProjectPath { get; set; }

        public string ApplicationInitializationServiceContent { get; set; }

        public string RibbonServiceContent { get; set; }

        public void Save()
        {
            var ApplicationInitializationServiceFile = Path.Combine(ProjectPath, "Services", "ApplicationInitializationService.cs");
            var RibbonServiceFile = Path.Combine(ProjectPath, "Services", "RibbonService.cs");

            ApplicationInitializationServiceContent.AddCopyright("ApplicationInitializationService.cs").SaveToFile(ApplicationInitializationServiceFile);
            RibbonServiceContent.AddCopyright("RibbonService.cs").SaveToFile(RibbonServiceFile);
        }

    }
}
