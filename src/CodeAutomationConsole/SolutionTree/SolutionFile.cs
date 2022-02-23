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
    public SolutionFile(string path, AutomationSettings settings)
    : base(settings)
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

        File.WriteAllText(path, Content);
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
        var template = Template.Parse(Content);

        var model = (ScriptObject)Context;
        var root = (ScriptObject)this.GetRoot().Context;
        
        if (!model.ContainsKey("root"))
        {
            model.Add("root", root.Clone(true));
        }

        model.Import(typeof(CustomFunctions));

        Content = template.Render(model);
    }
}