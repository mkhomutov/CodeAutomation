namespace CodeAutomationConsole
{
    using System.IO;
    using System.Xml.Linq;

    public class App
    {
        public App(string ns, string projectPath)
        {
            ProjectPath = projectPath;

            XNamespace defaultNS = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            XNamespace x = "http://schemas.microsoft.com/winfx/2006/xaml";

            var xml = new XElement(defaultNS + "Application",
                new XAttribute(XNamespace.Xmlns + "x", x),
                new XAttribute(x + "Class", $"{ns}.App"),
                    new XElement(defaultNS + "Application.Resources",
                        new XElement(defaultNS + "ResourceDictionary",
                            new XElement(defaultNS + "ResourceDictionary.MergedDictionaries",
                                new XElement(defaultNS + "ResourceDictionary", new XAttribute("Source", "/Themes/Generic.xaml"))
                            ),
                            new XElement(defaultNS + "SolidColorBrush", new XAttribute(x + "Key", "AccentColorBrush"), new XAttribute("Color", "Orange"))
                        )
                    )
                );

            ViewContent = xml.ToString();

            ViewCsContent = @$"
namespace {ns}
{{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using Catel.IoC;
    using Orchestra.Services;
    using Orchestra.Views;

    public partial class App : Application
    {{
        #region Constants
        #endregion

        #region Fields
        #endregion

        #region Constructors
        public App()
        {{

        }}
        #endregion

        #region Methods
        protected override void OnStartup(StartupEventArgs e)
        {{
            Orc.Theming.FontImage.RegisterFont(""FontAwesome"", new FontFamily(new System.Uri(""pack://application:,,,/{ns};component/Resources/Fonts/"", UriKind.RelativeOrAbsolute), ""./#FontAwesome""));
            Orc.Theming.FontImage.DefaultFontFamily = ""FontAwesome"";

            var serviceLocator = ServiceLocator.Default;
            var shellService = serviceLocator.ResolveType<IShellService>();
            shellService.CreateAsync<ShellWindow>();
        }}
        #endregion
    }}
}}
";
        }

        public string ProjectPath { get; set; }

        public string ViewContent { get; set; }

        public string ViewCsContent { get; set; }

        public void Save()
        {
            var viewFile = Path.Combine(ProjectPath, "App.xaml");
            var viewCsFile = Path.Combine(ProjectPath, "App.xaml.cs");

            ViewContent.SaveToFile(viewFile);
            ViewCsContent.SaveToFile(viewCsFile);
        }

    }
}
