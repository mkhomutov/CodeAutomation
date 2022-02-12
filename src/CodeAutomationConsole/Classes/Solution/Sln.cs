namespace CodeAutomationConsole
{
    using System;
    using System.IO;

    public static class Sln
    {
        public static string Content = CodeTemplate.GetByName("SolutionName.sln").
            Replace("%PROJECTGUID2%", Global.ProjectGuid).
            Replace("%PROJECTGUID1%", Guid.NewGuid().ToString().ToUpper()).
            Replace("%SOLUTIONGUID%", Guid.NewGuid().ToString().ToUpper());

        public static void Save()
        {
            var fileName = Path.Combine(Path.GetDirectoryName(Global.Path), $"{Global.Namespace}.sln");

            Content.SaveToFile(fileName);
        }

    }
}
