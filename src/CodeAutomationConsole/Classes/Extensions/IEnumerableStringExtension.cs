namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableStringExtension
    {
        public static string JoinWithTabs(this IEnumerable<string> strings, int count)
        {
            var tabs = string.Concat(Enumerable.Repeat("\t", count));

            var tabbedStrings = strings.Aggregate((x, y) => $"{x}\r\n{tabs}{y}");

            return tabbedStrings;
        }
    }
}
