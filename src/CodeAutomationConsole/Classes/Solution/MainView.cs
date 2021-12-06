namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class MainView : ViewTemplate
    {
        public MainView(string ns, string projectPath) : base(projectPath, "MainView")
        {
            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{ns}.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XAttribute(XNamespace.Xmlns + "orccontrols", Orccontrols),
                new XElement(Default + "Grid", new XComment("Under construction"))
                );

            ViewContent = xml.ToString();

            ViewCsContent = @$"
namespace {ns}.Views
{{
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
}}
";
            ViewModelContent = $@"
namespace {ns}.ViewModels
{{
    using Catel.MVVM;

    public class MainViewModel : ViewModelBase
    {{
    }}
}}
";
        }
    }
}
