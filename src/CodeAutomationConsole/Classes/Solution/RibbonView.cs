namespace CodeAutomationConsole
{
    using System.Linq;
    using System.Xml.Linq;

    public class RibbonView : ViewTemplate
    {
        public RibbonView(string ns, string projectPath, FluentRibbon ribbon) : base(projectPath, "RibbonView")
        {
            var projectName = ns.Split('.').LastOrDefault().ToLower();

            XNamespace projectNameSpace = "clr-namespace:" + ns;

            var xName = XName.Get("Name", X.ToString());

            var tabs = new XElement(Fluent + "Ribbon.Tabs", ribbon.Tabs.Select(tab => tab.TabItems.Select(tabItem =>
                new XElement(Fluent + "RibbonTabItem",
                    new XAttribute("Header", tabItem.Header), tabItem.GroupBoxes.Select(groupBox =>
                        new XElement(Fluent + "RibbonGroupBox", new XAttribute("Header", groupBox.Header), groupBox.Buttons.Select(button =>
                        {
                            var header = new XAttribute("Header", button.Header);
                            var icon = new XAttribute("LargeIcon", $"{{orctheming:FontImage {{x:Static {projectName}:FontAwesome.{button.LargeIcon}}}}}");
                            var command = button.Command is null ? null : new XAttribute("Command", $"{button.Command}");
                            return new XElement(Fluent + "Button", header, icon, command);
                        }
                                        )
                                    )
                                )
                            )
                        )
                    )
                );

            var backstage = new XElement(Fluent + "Ribbon.Menu",
                new XElement(Fluent + "Backstage",
                    new XElement(Fluent + "BackstageTabControl",
                    new XAttribute(xName, "backstageTabControl")
                        )
                    )
                );

            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "fluent", Fluent),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XAttribute(XNamespace.Xmlns + "orccontrols", Orccontrols),
                new XAttribute(XNamespace.Xmlns + "orctheming", Orctheming),
                new XAttribute(XNamespace.Xmlns + projectName, projectNameSpace),
                new XElement(Default + "Grid",
                    new XElement(Fluent + "Ribbon",
                    new XAttribute(xName, "ribbon"),
                    new XAttribute("IsQuickAccessToolBarVisible", "False"),
                    new XAttribute("CanCustomizeRibbon", "False"),
                    new XAttribute("AutomaticStateManagement", "False"),
                        backstage,
                        tabs
                        )
                    )
                );

            ViewContent = xml.ToString();

            ViewCsContent = @$"
namespace {ns}.Views
{{
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
}}
";
            ViewModelContent = $@"
namespace {ns}.ViewModels
{{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM;
    using Catel.Reflection;

    public class RibbonViewModel : ViewModelBase
    {{
        public RibbonViewModel()
        {{
            var assembly = AssemblyHelper.GetEntryAssembly();
            Title = assembly.Title();
        }}
    }}
}}
";
        }
    }
}
