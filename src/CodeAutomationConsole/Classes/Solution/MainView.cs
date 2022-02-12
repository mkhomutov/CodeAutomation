namespace CodeAutomationConsole
{
    using System.IO;

    public class MainView : ViewTemplate
    {
        public MainView() : base(Path.Combine(Global.Path, "UI"), "MainView")
        {
            ViewContent = CodeTemplate.GetByName("MainView.xaml").Replace("%PROJECTNAMESPACE%", Global.Namespace);

            ViewCsContent = CodeTemplate.GetByName("MainView.xaml.cs").Replace("%PROJECTNAMESPACE%", Global.Namespace);

            ViewModelContent = CodeTemplate.GetByName("MainViewModel.cs").Replace("%PROJECTNAMESPACE%", Global.Namespace);
        }
    }
}
