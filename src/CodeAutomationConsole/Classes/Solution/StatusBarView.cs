namespace CodeAutomationConsole
{
    using System.IO;
    using System.Xml.Linq;

    public class StatusBarView : ViewTemplate
    {
        public StatusBarView(string ns, string projectPath) : base(Path.Combine(projectPath, "UI"), "StatusBarView")
        {
            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.UI.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XElement(Default + "Grid", new XAttribute("HorizontalAlignment", "Right"), new XComment("Under construction"))
                );

            ViewContent = xml.ToString();

            ViewCsContent = @$"
namespace {ns}.UI.Views;

public partial class StatusBarView
{{
    #region Constructors
    public StatusBarView()
    {{
        InitializeComponent();
    }}
    #endregion
}}
";
            ViewModelContent = $@"
using Catel.MVVM;

namespace {ns}.UI.ViewModels;

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
";
        }

    }
}
