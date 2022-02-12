using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper.Configuration.Attributes;

namespace CodeAutomationConsole;

public class SolutionDirectory : SolutionItem
{
    private readonly SolutionItemsFactory _solutionItemsFactory;

    public SolutionDirectory(SolutionItemsFactory solutionItemsFactory, string path)
    {
        _solutionItemsFactory = solutionItemsFactory;
        Name = Path.GetFileName(path);

        var subDirectories = Directory.GetDirectories(path).Select(solutionItemsFactory.CreateSolutionDirectory);
        var files = Directory.GetFiles(path).Select(solutionItemsFactory.CreateSolutionFile);

        foreach (var solutionDirectory in subDirectories)
        {
            AddChild(solutionDirectory);
        }

        foreach (var solutionFile in files)
        {
            AddChild(solutionFile);
        }
    }

    private SolutionDirectory(SolutionDirectory obj)
        : base(obj)
    {
        _solutionItemsFactory = obj._solutionItemsFactory;
    }

    protected virtual bool IsTemplatesRoot => false;

    public sealed override void Save(string path)
    {
        if (IsFileSystemTemplate)
        {
            return;
        }

        if (!IsTemplatesRoot)
        {
            path = Path.Combine(path, Name);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        foreach (var child in Children)
        {
            child.Save(path);
        }
    }

    public override object Clone()
    {
        return new SolutionDirectory(this);
    }
}