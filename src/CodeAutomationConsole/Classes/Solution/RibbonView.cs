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

            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "fluent", Fluent),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XAttribute(XNamespace.Xmlns + "orchestra", Orchestra),
                new XAttribute(XNamespace.Xmlns + "orctheming", Orctheming),
                new XAttribute(XNamespace.Xmlns + projectName, projectNameSpace),
                new XElement(Default + "Grid",
                    ribbon.GetXml(ns)
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
