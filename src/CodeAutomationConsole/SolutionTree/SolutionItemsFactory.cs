using System.IO;

namespace CodeAutomationConsole;

public class SolutionItemsFactory
{
    public SolutionDirectory CreateSolutionDirectory(string path)
    {
        return new SolutionDirectory(this, path);
    }

    public SolutionFile CreateSolutionFile(string path)
    {
        if (Directory.Exists(path))
        {
        }

        return new SolutionFile(path);
    }
}