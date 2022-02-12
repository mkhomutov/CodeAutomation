namespace CodeAutomationConsole
{
    using System.IO;

    public static class Csproj
    {
        public static string Content = CodeTemplate.GetByName("ProjectName.csproj").Replace("%PROJECTGUID2%", Global.ProjectGuid);

        public static void Save()
        {
            var fileName = Path.Combine(Global.Path, $"{Global.Namespace}.csproj");

            Content.SaveToFile(fileName);
        }

    }
}
