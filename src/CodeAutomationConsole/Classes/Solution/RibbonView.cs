namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class RibbonView : ViewTemplate
    {
        public RibbonView(string ns, string projectPath) : base(projectPath, "RibbonView")
        {
            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "fluent", Fluent),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XAttribute(XNamespace.Xmlns + "orccontrols", Orccontrols),
                new XElement(Default + "Grid",
                    new XElement(Fluent + "Ribbon",
                    new XAttribute(XNamespace.Xmlns + "x", "ribbon"),
                    new XAttribute("IsQuickAccessToolBarVisible", "False"),
                    new XAttribute("CanCustomizeRibbon", "False"),
                    new XAttribute("AutomaticStateManagement", "False"),
                        new XElement(Fluent + "Ribbon.Menu",
                            new XElement(Fluent + "Backstage", new XComment("Under construnction"))
                            ),
                            new XElement(Fluent + "Ribbon.Tabs",
                                new XElement(Fluent + "RibbonTabItem", new XAttribute("Header", "View"), new XComment("Under construnction")),
                                new XElement(Fluent + "RibbonTabItem", new XAttribute("Header", "View2"), new XComment("Under construnction"))
                            )
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
