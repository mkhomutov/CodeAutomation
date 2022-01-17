namespace CodeAutomationConsole
{
    using System.IO;

    public static class ProjectApplicationInfo
    {
        private static readonly string Content = Template.GetByName("ProjectApplicationInfo.cs");
        public static void Save()
        {
            var projectViewConfigurationFile = Path.Combine(Global.Path, "ProjectApplicationInfo.cs");

            Content.SaveToFile(projectViewConfigurationFile);
        }
    }
}
