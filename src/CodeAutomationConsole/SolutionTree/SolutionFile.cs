using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Scriban;
using Scriban.Runtime;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CodeAutomationConsole;

public class SolutionFile : SolutionItem
{
    private readonly string _sourcePath;
    
    private string _renderedContent;

    public SolutionFile(string sourcePath, AutomationSettings settings)
    : base(settings)
    {
        _sourcePath = sourcePath;
        Name = Path.GetFileName(sourcePath);
    }

    private SolutionFile(SolutionFile obj)
        : base(obj)
    {
        _sourcePath = obj._sourcePath;
    }

    public sealed override void Save(string path)
    {
        if (IsFileSystemTemplate)
        {
            return;
        }

        var directoryPath = path;

        var fileName = GetFileName();

        path = Path.Combine(path, fileName);

        var exists = File.Exists(path);
        var isUpdateableFile = IsUpdateableFile();

        if (exists && isUpdateableFile)
        {
            File.Delete(path);
        }

        if (exists && !isUpdateableFile)
        {
            return;
        }
        
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if (_renderedContent is not null)
        {
            File.WriteAllText(path, _renderedContent);
        }
        else
        {
            File.Copy(_sourcePath, path);   
        }
    }

    private bool IsUpdateableFile()
    {
        var pattern = Settings.FileSystem.UpdateableFileMask.WildCardToRegular();
        return Regex.IsMatch(Name, pattern);
    }

    private string GetFileName()
    {
        var fileName = Name;
        var extension = Path.GetExtension(fileName);

        var resultExtension = Settings.FileSystem.Extensions.FirstOrDefault(x => string.Equals(x.Template, extension))?.Result;
        if (resultExtension is null)
        {
            return fileName;
        }

        return Path.ChangeExtension(fileName, resultExtension);
    }

    public override object Clone()
    {
        return new SolutionFile(this);
    }

    public override void RenderTemplate()
    {
        base.RenderTemplate();

        if (IsFileSystemTemplate)
        {
            return;
        }

        var extension = Path.GetExtension(Name);

        if (!Settings.FileSystem.Extensions.Any(x => string.Equals(x.Template, extension)))
        {
            return;
        }

        RenderContent();
    }

    private void RenderContent()
    {
        var content = File.ReadAllText(_sourcePath);
        var template = Template.Parse(content);

        var model = (ScriptObject)Context;
        var root = (ScriptObject)this.GetRoot().Context;
        
        if (!model.ContainsKey("root"))
        {
            model.Add("root", root.Clone(true));
        }

        model.Import(typeof(CustomFunctions));

        _renderedContent = template.Render(model);
    }
}