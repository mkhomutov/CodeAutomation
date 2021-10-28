namespace CodeAutomationConsole
{
    using Orc.CommandLine;

    public class CommandLineContext : ContextBase
    {
        public CommandLineContext()
        {
            GenerateProject = false;
            ConfigPath = string.Empty;
            GenerateYaml = false;
        }

        [Option("gp", "generate-project", AcceptsValue = false, HelpText = "Generate project")]
        public bool GenerateProject { get; set; }

        [Option("c", "config", HelpText = "Path to configuration file", TrimQuotes = true, TrimWhiteSpace = true)]
        public string ConfigPath { get; set; }

        [Option("gy", "generate-yaml", AcceptsValue = false, HelpText = "Generate YAML configuration for project")]
        public bool GenerateYaml { get; set; }
    }
}
