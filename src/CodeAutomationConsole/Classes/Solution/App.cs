namespace CodeAutomationConsole
{
    using System.IO;

    public static class App
    {
        public static void Save()
        {
            var viewContent = CodeTemplate.GetByName("App.xaml");
            var viewCsContent = CodeTemplate.GetByName("App.xaml.cs");

            var viewFile = Path.Combine(Global.Path, "App.xaml");
            var viewCsFile = Path.Combine(Global.Path, "App.xaml.cs");

            viewContent.SaveToFile(viewFile);
            viewCsContent.SaveToFile(viewCsFile);
        }

    }
}
