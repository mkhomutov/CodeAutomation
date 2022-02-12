namespace CodeAutomationConsole
{
    using System.IO;

    public static class AssemblyInfo
    {
        public static void Save()
        {
            var content = CodeTemplate.GetByName("AssemblyInfo.cs").Replace("%PROJECTNAMESPACE%", Global.Namespace);

            var fileName = Path.Combine(Global.Path, "Properties", "AssemblyInfo.cs");

            content.AddCopyright("AssemblyInfo.cs").SaveToFile(fileName);
        }
    }
}
