namespace CodeAutomationConsole
{
    using System.IO;
    using System.Xml.Linq;

    public class StatusBarView : ViewTemplate
    {
        public StatusBarView() : base(Path.Combine(Global.Path, "UI"), "StatusBarView")
        {
            ViewContent = Template.GetByName("StatusBarView.xaml");

            ViewCsContent = Template.GetByName("StatusBarView.xaml.cs");

            ViewModelContent = Template.GetByName("StatusBarViewModel.cs");
        }

    }
}
