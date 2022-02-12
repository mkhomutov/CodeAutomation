namespace CodeAutomationConsole
{
    using System.IO;

    public static class Commands
    {
        private static readonly string Filename = "ProjectOpenCommandContainer.cs";

        public static string Content = CodeTemplate.GetByName(Filename);

        public static void Save()
        {
            var file = Path.Combine(Global.Path, "Commands", Filename);

            Content.AddCopyright(Filename).SaveToFile(file);
        }
    }
}
