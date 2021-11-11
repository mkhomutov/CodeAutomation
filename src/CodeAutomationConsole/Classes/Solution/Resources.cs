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

            Prefix = $"{Assembly.GetName().Name}.Resources.";

            ResourceFiles = Assembly.GetManifestResourceNames().Where(x => x.StartsWith(Prefix)).ToArray();
        }

        public Assembly Assembly { get; set; }

        public string Prefix { get; set; }

        public string ProjectPath {get; set; }

        public IEnumerable ResourceFiles { get; set; }

        public void Save()
        {
            foreach (string resource in ResourceFiles)
            {
                var resourceName = resource.Substring(Prefix.Length);

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
        }
    }
}
