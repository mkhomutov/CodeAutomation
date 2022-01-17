namespace CodeAutomationConsole
{
    using System.IO;

    public class MainView : ViewTemplate
    {
        public MainView() : base(Path.Combine(Global.Path, "UI"), "MainView")
        {
            ViewContent = Template.GetByName("MainView.xaml").Replace("%PROJECTNAMESPACE%", Global.Namespace);

            ViewCsContent = Template.GetByName("MainView.xaml.cs").Replace("%PROJECTNAMESPACE%", Global.Namespace);

            ViewModelContent = Template.GetByName("MainViewModel.cs").Replace("%PROJECTNAMESPACE%", Global.Namespace);
        }
    }
}
