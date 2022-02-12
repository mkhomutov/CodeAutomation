namespace CodeAutomationConsole
{
    using System.IO;

    public static class ModuleInitializer
    {
        public static void Save()
        {
            var content = CodeTemplate.GetByName("ModuleInitializer.cs");

            var fileName = Path.Combine(Global.Path, "ModuleInitializer.cs");
            content.SaveToFile(fileName);
        }
    }
}
