using System.Collections.Generic;
using System.IO;
using System.Linq;
using Scriban;
using Scriban.Runtime;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CodeAutomationConsole;

public class SolutionFile : SolutionItem
{
    private static readonly Dictionary<string, string> _extensionByTemplateExtensions;

    static SolutionFile()
    {
        _extensionByTemplateExtensions = new Dictionary<string, string>
        {
            {".sbn-cs", ".cs"},
            {".sbn-sln", ".sln"},
            {".sbn-xaml", ".xaml"},
            {".sbn-csproj", ".csproj"},
            {".sbn-DotSettings", ".DotSettings"},
            {".sbn-cake", ".cake"},
            {".sbn-props", ".props"},
            {".sbn-json", ".json"},
        };
    }

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
        if (IsFileSystemTemplate)
        {
            return;
        }

        var directoryPath = path;

        var fileName = GetFileName();

        path = Path.Combine(path, fileName);

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

        SaveContext(directoryPath);
    }

    private string GetFileName()
    {
        var fileName = Name;
        var extension = Path.GetExtension(fileName);

        if (_extensionByTemplateExtensions.TryGetValue(extension ?? string.Empty, out var finalExtension))
        {
            return Path.ChangeExtension(fileName, finalExtension);
        }

        return fileName;
    }

    public void SaveContext(string path)
    {
        var fileName = Path.GetFileNameWithoutExtension(Name) + ".yaml";
        var fullName = Path.Combine(path, fileName);

        var serializer = new SerializerBuilder().
            WithNamingConvention(CamelCaseNamingConvention.Instance).
            ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).
            Build();

        var content = serializer.Serialize(Context);

        if (File.Exists(fullName))
        {
            File.Delete(fullName);
        }

        File.WriteAllText(fullName, content);
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

        if (!_extensionByTemplateExtensions.ContainsKey(Path.GetExtension(Name) ?? string.Empty))
        {
            return;
        }

        RenderContent();
    }

    private void RenderContent()
    {
        var template = Template.Parse(Content);

        var model = (ScriptObject)Context.FixTypes();
        model.Add("root", this.GetRoot().Context.FixTypes());
        model.Import(typeof(CustomFunctions));

        Content = template.Render(model);
    }
}