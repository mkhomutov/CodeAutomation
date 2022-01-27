using System.IO;

namespace CodeAutomationConsole;

public class SolutionFile : SolutionItem
{
    public SolutionFile(string path)
    {
        Name = Path.GetFileName(path);

        Content = File.ReadAllText(path);
    }

    private SolutionFile(SolutionFile obj)
        : base(obj)
    {
        Content = obj.Content;
    }

    public string Content { get; set; }

    public sealed override void Save(string path)
    {
        if (IsTemplate)
        {
            return;
        }

        path = Path.Combine(path, Name);

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, Content);
    }

    public override object Clone()
    {
        return new SolutionFile(this);
    }
}