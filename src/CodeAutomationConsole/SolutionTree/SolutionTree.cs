using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Catel;

namespace CodeAutomationConsole;

public class SolutionTree : SolutionDirectory
{
    private readonly SolutionItemsFactory _solutionItemsFactory;

    public SolutionTree(string templatesPath, object context, AutomationSettings settings)
        : base(new SolutionItemsFactory(settings), templatesPath, settings)
    {
        _solutionItemsFactory = new SolutionItemsFactory(settings);
        Context = context;
    }

    protected override bool IsTemplatesRoot => true;
}