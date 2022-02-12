using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeAutomationConsole;

public abstract class SolutionItem : ICloneable
{
    private readonly List<SolutionItem> _children = new List<SolutionItem>();

    protected static readonly ITemplateTranslator Translator = new MainTemplateResolver();

    protected SolutionItem()
    {
    }

    protected SolutionItem(SolutionItem obj)
    {
        Name = obj.Name;
        Context = obj.Context;

        foreach (var child in obj.Children)
        {
            var solutionItem = (SolutionItem)child.Clone();
            solutionItem.Context = child.Context;

            AddChild(solutionItem);
        }
    }

    public SolutionItem Parent { get; private set; }

    public string Name { get; protected set; }

    /// <summary>
    /// The part deserialized of yaml configuration, directly related to this SolutionItem
    /// </summary>
    public object Context { get; set; }

    protected bool IsFileSystemTemplate => Name.Contains("{{");
    public IReadOnlyCollection<SolutionItem> Children => _children;

    public abstract void Save(string path);

    public virtual void RenderTemplate()
    {
        if (IsFileSystemTemplate)
        {
            var translationContext = new TranslationContext
            {
                RootContext = this.GetRoot().Context,
                Context = Context,
                Argument = Name
            };

            var translationResults = Translator.Translate(translationContext);
            foreach (var translationResult in translationResults)
            {
                var solutionItem = (SolutionItem)Clone();
                solutionItem.Name = (string)translationResult.Value;
                solutionItem.Context = translationResult.Context;

                Parent.AddChild(solutionItem);

                solutionItem.RenderTemplate();
            }

            return;
        }

        foreach (var child in Children.ToList())
        {
            child.Context = Context;
            child.RenderTemplate();
        }
    }

    protected void AddChild(SolutionItem item)
    {
        item.Parent = this;
        _children.Add(item);
    }

    public override string ToString()
    {
        return Name;
    }

    public abstract object Clone();
}