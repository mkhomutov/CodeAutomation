namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class RibbonView : ViewTemplate
    {
        public RibbonView(string ns, string projectPath, FluentRibbon ribbon) : base(Path.Combine(projectPath, "UI"), "RibbonView")
        {
            var projectName = ns.Split('.').LastOrDefault().ToLower();

            XNamespace projectNameSpace = "clr-namespace:" + ns;

            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.UI.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "fluent", Fluent),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XAttribute(XNamespace.Xmlns + "orchestra", Orchestra),
                new XAttribute(XNamespace.Xmlns + "orctheming", Orctheming),
                new XAttribute(XNamespace.Xmlns + projectName, $"clr-namespace:{ns}"),
                new XAttribute(XNamespace.Xmlns + "gumui", GumUI),
                new XAttribute(XNamespace.Xmlns + "views", Views),
                new XElement(Default + "Grid",
                    new XElement(Fluent + "Ribbon",
                        new XAttribute(X + "Name", "ribbon"),
                        new XAttribute("IsQuickAccessToolBarVisible", "False"),
                        new XAttribute("CanCustomizeRibbon", "False"),
                        new XAttribute("AutomaticStateManagement", "False"),
                        new XElement(Fluent + "Ribbon.QuickAccessItems",
                            new XElement(Fluent + "QuickAccessMenuItem",
                                new XAttribute("Header", "Save"),
                                new XAttribute("Icon", "{orctheming:FontImage ItemName={x:Static gumui:WildGums.Save}}"),
                                new XAttribute("Command", "{catel:CommandManagerBinding Project.Save}")
                                )),
                        new XElement(Fluent + "Ribbon.Menu",
                            new XElement(Fluent + "Backstage",
                                new XAttribute("Header", "File"),
                                new XElement(Fluent + "BackstageTabControl",
                                    new XAttribute(X + "Name", "ribbonBackstageTabControl"),
                                    new XElement(Fluent + "BackstageTabItem",
                                        new XAttribute("Header", "Open"),
                                        new XAttribute("Visibility", "{Binding IsOpenProjectBackstageTabItemVisible, Converter={catel:BooleanToCollapsingVisibilityConverter}}"),
                                        new XElement(Views + "OpenProjectView", new XAttribute("Visibility", "{Binding IsOpenProjectViewVisible, Converter={catel:BooleanToCollapsingVisibilityConverter}}"))
                                        ),
                                    new XElement(Fluent + "Button",
                                        new XAttribute("Visibility", "{Binding IsSaveProjectBackstageButtonVisible, Converter={catel:BooleanToCollapsingVisibilityConverter}}"),
                                        new XAttribute("Header", "Save"),
                                        new XAttribute("Icon", "{orctheming:FontImage ItemName={x:Static gumui:WildGums.Save}}"),
                                        new XAttribute("Command", "{catel:CommandManagerBinding Project.Save}")
                                        ),
                                    new XElement(Fluent + "Button",
                                        new XAttribute("Visibility", "{Binding IsSaveAsProjectBackstageButtonVisible, Converter={catel:BooleanToCollapsingVisibilityConverter}}"),
                                        new XAttribute("Header", "Save as..."),
                                        new XAttribute("Icon", "{orctheming:FontImage ItemName={x:Static gumui:WildGums.Save}}"),
                                        new XAttribute("Command", "{catel:CommandManagerBinding Project.SaveAs}")
                                        ),
                                    new XElement(Fluent + "SeparatorTabItem"),
                                    new XElement(Fluent + "BackstageTabItem",
                                        new XAttribute("Header", "Info"),
                                        new XElement(Views + "InfoCenterView")
                                        ),
                                    new XElement(Fluent + "BackstageTabItem",
                                        new XAttribute("Header", "Support"),
                                        new XElement(Views + "SupportCenterView")
                                        ),
                                    new XElement(Fluent + "BackstageTabItem",
                                        new XAttribute("Header", "Settings"),
                                        new XElement(Views + "SettingsCenterView")
                                        ),
                                    new XElement(Fluent + "SeparatorTabItem"),
                                    new XElement(Fluent + "Button",
                                        new XAttribute("Visibility", "{Binding IsCloseProjectBackstageButtonVisible, Converter={catel:BooleanToCollapsingVisibilityConverter}}"),
                                        new XAttribute("Header", "Close"),
                                        new XAttribute("Command", "{catel:CommandManagerBinding Project.Close}")
                                        )
                                    )
                                )
                            ),
                        ribbon.Tabs.Select(tab => tab.GetXml(ns))
                        )
                    )
                );

            ViewContent = xml.ToString().FormatXaml();

            ViewCsContent = @$"
namespace {ns}.UI.Views;

public partial class RibbonView
{{
    #region Constructors
    public RibbonView()
    {{
        InitializeComponent();
    }}
    #endregion

    #region Methods

    #endregion
}}
";
            ViewModelContent = $@"
using Catel.Fody;
using Catel.MVVM;
using Catel.Reflection;
using {ns}.UI.Models;

namespace {ns}.UI.ViewModels;

public sealed class RibbonViewModel : ViewModelBase
{{
    public RibbonViewModel()
    {{
        var assembly = AssemblyHelper.GetEntryAssembly();
        Title = assembly.Title();

        ProjectViewConfiguration = new ProjectViewConfiguration()
        {{
            IsOpenProjectBackstageTabItemVisible = true,
            IsOpenProjectViewVisible = true,
            IsSaveProjectBackstageButtonVisible = false,
            IsSaveAsProjectBackstageButtonVisible = false,
            IsCloseProjectBackstageButtonVisible = false,
        }};
    }}

    [Model]
    [Expose(nameof(Models.ProjectViewConfiguration.IsOpenProjectBackstageTabItemVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsOpenProjectViewVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsSaveProjectBackstageButtonVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsSaveAsProjectBackstageButtonVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsCloseProjectBackstageButtonVisible))]
    public ProjectViewConfiguration ProjectViewConfiguration {{ get; set; }}
}}
";
        }
    }
}
