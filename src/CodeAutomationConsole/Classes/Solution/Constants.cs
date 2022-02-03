namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;

    public static class Constants
    {
        public static void Save()
        {
            var scopeNames = Global.Config.CsvList.Select(x => $"public const string {x.ClassName} = \"{x.ClassName}\";").
                ToArray().
                JoinWithTabs(1);
            var fileNames = Global.Config.CsvList.Select(x => $"public const string {x.File} = \"{x.File}.csv\";").
                ToArray().
                JoinWithTabs(1);

            var content = Template.GetByName("Constants.cs").
                Replace("%SCOPENAMES%", scopeNames).
                Replace("%FILENAMES%", fileNames);

            var file = Path.Combine(Global.Path, "Constants.cs");

            content.SaveToFile(file);
        }
    }
}
