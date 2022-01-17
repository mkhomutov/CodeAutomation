namespace CodeAutomationConsole
{
    using System.IO;

    public static class FontAwesome
    {
        public static void Save()
        {
            var content = Template.GetByName("FontAwesome.cs");

            var fileName = Path.Combine(Global.Path, "FontAwesome.cs");

            content.SaveToFile(fileName);
        }
    }
}
