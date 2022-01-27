using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeAutomationConsole;

public abstract class SolutionItem : ICloneable
{
    private readonly List<SolutionItem> _children = new List<SolutionItem>();

    private static readonly ITemplateTranslator Translator = new MainTemplateTranslator();

    protected SolutionItem()
    {
    }

    protected SolutionItem(SolutionItem obj)
    {
        Name = obj.Name;
        foreach (var child in obj.Children)
        {
            AddChild((SolutionItem)child.Clone());
        }
    }

    public SolutionItem Parent { get; private set; }

    public string Name { get; protected set; }

    /// <summary>
    /// The part deserialized of yaml configuration, directly related to this SolutionItem
    /// </summary>
    public object Context { get; set; }

    protected bool IsTemplate => Name.StartsWith('#');
    public IReadOnlyCollection<SolutionItem> Children => _children;

    public abstract void Save(string path);

    public void TranslateTemplate()
    {

        if (IsTemplate)
        {
            var translationContext = new TranslationContext
            {
                SolutionItem = this,
                Text = Name
            };

            var translationResults = Translator.Translate(translationContext);
            foreach (var translationResult in translationResults)
            {
                var solutionItem = (SolutionItem)Clone();
                solutionItem.Name = translationResult.TranslatedText;
                solutionItem.Context = translationResult.Context;

                Parent.AddChild(solutionItem);

                solutionItem.TranslateTemplate();
            }

            return;
        }

        foreach (var child in Children.ToList())
        {
            child.Context = Context;
            child.TranslateTemplate();
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