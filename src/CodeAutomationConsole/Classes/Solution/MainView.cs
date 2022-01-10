namespace CodeAutomationConsole
{
    using System.IO;
    using System.Xml.Linq;

    public class MainView : ViewTemplate
    {
        public MainView(string ns, string projectPath) : base(Path.Combine(projectPath, "UI"), "MainView")
        {
            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.UI.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XAttribute(XNamespace.Xmlns + "orccontrols", Orccontrols),
                new XAttribute(XNamespace.Xmlns + "views", Views),
                new XElement(Views + "TabsHostView", 
                    new XAttribute("SingleActiveContent", "False"))
                );

            ViewContent = xml.ToString();

            ViewCsContent = @$"
namespace {ns}.UI.Views;

/// <summary>
/// Interaction logic for MainView.xaml
/// </summary>
public partial class MainView
{{
    #region Constructors
    public MainView()
    {{
        InitializeComponent();
    }}
    #endregion
}}
";
            ViewModelContent = $@"
using Catel.MVVM;

namespace {ns}.UI.ViewModels;

public class MainViewModel : ViewModelBase
{{
}}
";
        }
    }
}
