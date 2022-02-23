using System.IO;

namespace CodeAutomationConsole;

public class SolutionItemsFactory
{
    private readonly AutomationSettings _settings;

    public SolutionItemsFactory(AutomationSettings settings)
    {
        _settings = settings;
    }

    public SolutionDirectory CreateSolutionDirectory(string path)
    {
        return new SolutionDirectory(this, path, _settings);
    }

    public SolutionFile CreateSolutionFile(string path)
    {
        if (Directory.Exists(path))
        {
        }

        return new SolutionFile(path, _settings);
    }
}