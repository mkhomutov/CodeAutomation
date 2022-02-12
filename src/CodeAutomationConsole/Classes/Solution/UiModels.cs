namespace CodeAutomationConsole
{
    using System.IO;

    public static class UiModels
    {
        private static readonly string Filename = "ProjectViewConfiguration.cs";

        private static readonly string ProjectViewConfiguration = CodeTemplate.GetByName(Filename).
            Replace("%PROJECTNAMESPACE%", Global.Namespace);

        public static void Save()
        {
            var projectViewConfigurationFile = Path.Combine(Global.Path, "UI", "Models", Filename);

            ProjectViewConfiguration.SaveToFile(projectViewConfigurationFile);
        }
    }
}
