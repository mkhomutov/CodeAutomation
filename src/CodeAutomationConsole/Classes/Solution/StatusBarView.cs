namespace CodeAutomationConsole
{
    using System.IO;
    using System.Xml.Linq;

    public class StatusBarView : ViewTemplate
    {
        public StatusBarView() : base(Path.Combine(Global.Path, "UI"), "StatusBarView")
        {
            ViewContent = CodeTemplate.GetByName("StatusBarView.xaml");

            ViewCsContent = CodeTemplate.GetByName("StatusBarView.xaml.cs");

            ViewModelContent = CodeTemplate.GetByName("StatusBarViewModel.cs");
        }

    }
}
