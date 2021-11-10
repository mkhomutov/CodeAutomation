namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using Catel.IoC;
    using Orc.CommandLine;

    public class Program
    {
        public static void Main(string[] args)
        {
            // Commandline processing
            var commandLine = Environment.CommandLine.GetCommandLine();

            var commandLineContext = new CommandLineContext();

            var parser = CreateCommandLineParser();

            var validationContext = parser.Parse(commandLine, commandLineContext);

            ProcessCommndLine(commandLineContext, validationContext, parser);

            // Load configuration
            var config = new LoadConfiguration(commandLineContext.ConfigPath).Config;

            var yamlPath = Path.Combine(config.ProjectPath, $"{config.ProjectName}.yaml");

            // Generate YAML config
            if (commandLineContext.GenerateYaml)
            {
                var yaml = new CreateYaml(config);

                yaml.SaveTo(yamlPath);

                Console.WriteLine($"Yaml config for project {config.ProjectName} is generated to {yamlPath}");
            }

            // Generate project
            if (commandLineContext.GenerateProject)
            {
                var project = new Project(yamlPath);

                project.Generate();
            }
        }

        private static void ProcessCommndLine(CommandLineContext commandLineContext, Catel.Data.IValidationContext validationContext, ICommandLineParser parser)
        {
            if (validationContext.HasWarnings)
            {
                Console.WriteLine("Some warnings");
            }

            if (validationContext.HasErrors)
            {
                Console.WriteLine("Some errors");
                Environment.Exit(0);
            }

            if (commandLineContext.IsHelp)
            {
                Console.WriteLine(String.Join('\n', parser.GetHelp(commandLineContext)));

                Environment.Exit(0);
            }

            if (!File.Exists(commandLineContext.ConfigPath))
            {
                Console.WriteLine($"Configuration file {commandLineContext.ConfigPath} not found");
                Environment.Exit(0);
            }

            if(!commandLineContext.GenerateProject && !commandLineContext.GenerateYaml)
            {
                Console.WriteLine("Please use options /gy or /gp");
                Console.WriteLine(String.Join('\n', parser.GetHelp(commandLineContext)));
                Environment.Exit(0);
            }
        }

        private static ICommandLineParser CreateCommandLineParser()
        {
            var serviceLocator = ServiceLocator.Default;

            var typeFactory = serviceLocator.ResolveType<ITypeFactory>();

            return typeFactory.CreateInstanceWithParametersAndAutoCompletion<CommandLineParser>(new OptionDefinitionService());
        }
    }
}
