namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class StatusBarView : ViewTemplate
    {
        public StatusBarView(string ns, string projectPath) : base(projectPath, "StatusBarView")
        {
            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XElement(Default + "Grid", new XAttribute("HorizontalAlignment", "Right"), new XComment("Under construction"))
                );

            ViewContent = xml.ToString();

            ViewCsContent = @$"
namespace {ns}.Views
{{
    public partial class StatusBarView
    {{
        #region Constructors
        public StatusBarView()
        {{
            InitializeComponent();
        }}
        #endregion
    }}
}}
";
            ViewModelContent = $@"
namespace {ns}.ViewModels
{{
    using Catel.MVVM;

    public class StatusBarViewModel : ViewModelBase
    {{
        #region Properties
        public override string Title
        {{
            get {{ return ""Status bar title binding""; }}
        }}
        #endregion

        public bool EnableAutomaticUpdates {{ get; set; }}
    }}
}}
";
        }

    }
}
