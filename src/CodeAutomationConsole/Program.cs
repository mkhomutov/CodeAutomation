namespace CodeAutomationConsole
{
    using System;
    using System.IO;

    public class Program
    {
        public static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please use path to the YAML file as a command line argument");
                return 1;
            }

            var path = args[0];
            var configFile = new FileInfo(path);

            if (!configFile.Exists)
            {
                Console.WriteLine("Please make sure that configuration file exists");
                return 1;
            }

            var settings = AutomationSettings.Load(configFile.FullName);
            var extendedSettingsFile = Path.Combine(settings.OutputPath, "config.yaml");
            if (File.Exists(extendedSettingsFile))
            {
                settings = AutomationSettings.Load(extendedSettingsFile);
            }

            var codeMaker = new CodeBuilder(settings);
            codeMaker.Run();

            return 0;
        }
    }
}
