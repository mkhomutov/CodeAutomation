namespace CodeAutomationConsole;

public static class SolutionItemExtensions
{
    public static SolutionItem GetRoot(this SolutionItem solutionItem)
    {
        if (solutionItem is null)
        {
            return null;
        }

        var parent = solutionItem.Parent;
        if (parent is null)
        {
            return solutionItem;
        }

        return parent.GetRoot();
    }
}