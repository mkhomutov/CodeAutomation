using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Catel;

namespace CodeAutomationConsole;

public class SolutionTree : SolutionDirectory
{
    private readonly SolutionItemsFactory _solutionItemsFactory = new SolutionItemsFactory();

    public SolutionTree(string templatesPath, object context)
        : base(new SolutionItemsFactory(), templatesPath)
    {
        Context = context;
    }

    protected override bool IsTemplatesRoot => true;
}