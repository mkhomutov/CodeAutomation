namespace CodeAutomationConsole
{
    using System.IO;

    public static class ModuleInitializer
    {
        public static void Save()
        {
            var content = Template.GetByName("Constants.cs");

            var fileName = Path.Combine(Global.Path, "Constants.cs");
            content.SaveToFile(fileName);
        }
    }
}
