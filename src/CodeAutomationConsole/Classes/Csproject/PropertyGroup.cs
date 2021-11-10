namespace CodeAutomationConsole
{
    public class PropertyGroup
    {

        public PropertyGroup() : this("", "")
        {

        }
        public PropertyGroup(string name, string root)
        {
            TargetFramework = "net5.0";
            OutputType = "exe";
            AssemblyName = name;
            RootNamespace = root;
            DefaultLanguage = "en-US";
        }

        public string TargetFramework { get; set; }
        public string OutputType { get; set; }
        public string AssemblyName { get; set; }
        public string RootNamespace { get; set; }
        public string DefaultLanguage { get; set; }
    }
}
