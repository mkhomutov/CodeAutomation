namespace CodeAutomationConsole
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public class Resources
    {
        public Resources(string projectPath)
        {
            ProjectPath = projectPath;

            Assembly = Assembly.GetExecutingAssembly();

            ProjectResourcesPrefix = $"{Assembly.GetName().Name}.ProjectResources.";
            SolutionResourcesPrefix = $"{Assembly.GetName().Name}.SolutionResources.";

            ProjectResourceFiles = Assembly.GetManifestResourceNames().Where(x => x.StartsWith(ProjectResourcesPrefix)).ToArray();
            SolutionResourceFiles = Assembly.GetManifestResourceNames().Where(x => x.StartsWith(SolutionResourcesPrefix)).ToArray();

        }

        public Assembly Assembly { get; set; }

        public string ProjectResourcesPrefix { get; set; }

        public string SolutionResourcesPrefix { get; set; }

        public string ProjectPath {get; set; }

        public IEnumerable ProjectResourceFiles { get; set; }

        public IEnumerable SolutionResourceFiles { get; set; }

        public void Save()
        {
            // create 
            foreach (string resource in ProjectResourceFiles)
            {
                var resourceName = resource.Substring(ProjectResourcesPrefix.Length);

                var filename = String.Join('.', resourceName.Split('.').TakeLast(2));

                var relativePath = String.Join('\\', resourceName.Split('.').SkipLast(2));
                var path = Path.Combine(ProjectPath, relativePath);

                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                var fullPath = Path.Combine(path, filename);

                using (var stream = Assembly.GetManifestResourceStream(resource))
                using (var fstream = new FileStream(fullPath, FileMode.Create))
                {
                    byte[] array = new byte[stream.Length];
                    stream.Read(array, 0, array.Length);
                    fstream.Write(array, 0, array.Length);
                }
            }

            // Create static service files in the solution directory
            foreach (string resource in SolutionResourceFiles)
            {
                var resourceName = resource.Substring(SolutionResourcesPrefix.Length);

                var fullPath = Path.Combine(Global.Config.ExportPath, "src", resourceName);

                using (var stream = Assembly.GetManifestResourceStream(resource))
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
