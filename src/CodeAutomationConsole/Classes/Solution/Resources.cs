namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Reflection;
    using System.IO;

    public static class Resources
    {
        public static XElement IncludeResources { get; set; }
        public static void Save()
        {
            var projectPath = Global.Path;

            var assembly = Assembly.GetExecutingAssembly();

            var projectResourcesPrefix = $"{assembly.GetName().Name}.ProjectResources.";
            var solutionResourcesPrefix = $"{assembly.GetName().Name}.SolutionResources.";

            var projectResourceFiles = assembly.GetManifestResourceNames().Where(x => x.StartsWith(projectResourcesPrefix)).ToArray();
            var solutionResourceFiles = assembly.GetManifestResourceNames().Where(x => x.StartsWith(solutionResourcesPrefix)).ToArray();

            IncludeResources = new XElement("ItemGroup");

            // create project resources
            foreach (string resource in projectResourceFiles)
            {
                var resourceName = resource.Substring(projectResourcesPrefix.Length);

                var filename = String.Join('.', resourceName.Split('.').TakeLast(2));

                var relativePath = String.Join('\\', resourceName.Split('.').SkipLast(2));
                var path = Path.Combine(projectPath, relativePath);

                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                var fullPath = Path.Combine(path, filename);

                using (var stream = assembly.GetManifestResourceStream(resource))
                using (var fstream = new FileStream(fullPath, FileMode.Create))
                {
                    byte[] array = new byte[stream.Length];
                    stream.Read(array, 0, array.Length);
                    fstream.Write(array, 0, array.Length);
                }

                // prepare project includes
                if (relativePath.StartsWith("Resources\\"))
                {
                    IncludeResources.Add(new XElement("Resource", new XAttribute("Include", $"{relativePath}\\{filename}")));
                }

            }

            // Create static service files in the solution directory
            foreach (string resource in solutionResourceFiles)
            {
                var resourceName = resource.Substring(solutionResourcesPrefix.Length);

                var fullPath = Path.Combine(Global.Config.ExportPath, "src", resourceName);

                using (var stream = assembly.GetManifestResourceStream(resource))
                using (var fstream = new FileStream(fullPath, FileMode.Create))
                {
                    byte[] array = new byte[stream.Length];
                    stream.Read(array, 0, array.Length);
                    fstream.Write(array, 0, array.Length);
                }
            }
        }
    }
}
