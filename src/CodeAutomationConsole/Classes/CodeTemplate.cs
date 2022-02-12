namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public static class CodeTemplate
    {
        public static string GetByName(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var templatesPrefix = $"{assembly.GetName().Name}.Templates.";
            var templateFile = assembly.GetManifestResourceNames().
                Where(x => x.StartsWith(templatesPrefix)).
                Where(x => x.EndsWith(name)).
                FirstOrDefault();

            using (var stream = assembly.GetManifestResourceStream(templateFile))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd().Replace("%PROJECTNAMESPACE%", Global.Namespace);
            }
        }
    }
}
