namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.CommandLine;
    using System.CommandLine.Invocation;

    public class Program
    {
        public static int Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                new Option<FileInfo>(
                    new[] { "/c", "--config"},
                    "Path to configuration file"),
                new Option<bool>(
                    new[] { "/y", "--generate-only-yaml"},
                    getDefaultValue: () => false,
                    description: "Generate YAML-description for project"),
                new Option<bool>(
                    new[] { "/p", "--generate-project"},
                    getDefaultValue: () => false,
                    description: "Generate project"),

            };

            rootCommand.Description = "Code automation App";

            rootCommand.Handler = CommandHandler.Create<FileInfo, bool, bool>((config, generateProject, generateOnlyYaml) =>
            {
                switch (config?.Exists ?? null)
                {
                    case null:
                        Console.WriteLine("Please use \"--config\" option to specify configuration file");
                        break;
                    case false:
                        Console.WriteLine("Please make sure that configuration file exists");
                        break;
                    case true:
                        var configuration = new LoadBaseConfiguration(config.FullName).Config;

                        var yamlPath = Path.Combine(configuration.ProjectPath, $"{configuration.ProjectName}.yaml");

                        if ( generateOnlyYaml || (generateProject && !File.Exists(yamlPath) ) )
                        {
                            new CreateExtendedConfiguration(configuration).Content.SaveToFile(yamlPath);
                        }

                        if (generateProject)
                        {
                            new Solution(yamlPath).Generate();
                        }

                        break;
                }
            });

            return rootCommand.InvokeAsync(args).Result;
        }
    }
}
