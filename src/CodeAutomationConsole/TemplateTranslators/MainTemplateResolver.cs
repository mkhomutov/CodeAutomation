using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutomationConsole
{
    public class MainTemplateResolver : TemplateResolverBase
    {
        private readonly Dictionary<string, ITemplateTranslator> _templateTranslators;

        public MainTemplateResolver()
        {
            _templateTranslators = new Dictionary<string, ITemplateTranslator>()
            {
                { "Guid", new GuidTemplateResolver() },
                { "AssemblyName", new AssemblyNameTemplateResolver() },
                { "Namespace", new NamespaceTemplateResolver() },
                { "CsvMap", new CsvMapTemplateResolver() },
                { "CsvModelProperties", new CsvModelPropertiesTemplateResolver() },
                { "MasterData", new MasterDataTemplateResolver() },
            };
        }

        public override IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters)
        {
            var rootContext = fallbackTarget;
            var context = target;
            var template = parameters[0];

            var dictionary = new Dictionary<string, IReadOnlyCollection<SettingValue>>();

            var arguments = ExtractTemplateArguments(template);
            foreach (var argument in arguments)
            {
                var parser = GetCustomResolver(argument);
                var resolverParameters = GetResolverParameters(argument);

                if (parser is null)
                {
                    Console.WriteLine($"WARNING: Cannot resolve expression {argument}");
                    continue;
                }

                dictionary["{{" + argument + "}}"] = parser.Resolve(context,  rootContext, resolverParameters).ToList();
            }

            var resultsCount = 1;
            foreach (var value in dictionary.Select(x => new { Key = x.Key, Count = x.Value.Count}).ToArray())
            {
                var count = value.Count;
                if (count == 0)
                {
                    Console.WriteLine($"WARNING: Cannot find value for '{value.Key}'");
                    dictionary.Remove(value.Key);
                    continue;
                }

                resultsCount *= count;
            }

            var finalResult = new List<SettingValue>();

            for (var i = 0; i < resultsCount; i++)
            {
                finalResult.Add(new SettingValue
                {
                    Value = template,
                    Context = context
                });
            }

            foreach (var keyValuePair in dictionary)
            {
                var key = keyValuePair.Key;

                var index = 0;
                while (index < finalResult.Count)
                {
                    foreach (var item in keyValuePair.Value)
                    {
                        var settingValue = finalResult[index++];
                        settingValue.Value = ReplaceSettingValue(((string)settingValue.Value), key, (string)item.Value);
                        settingValue.Context = item.Context;
                    }
                }
            }

            return finalResult;
        }

        private static string ReplaceSettingValue(string textForReplace, string textToFind, string textToInsert)
        {
            var firstMatchIndex = textForReplace.IndexOf(textToFind, StringComparison.Ordinal);
            if (firstMatchIndex == -1)
            {
                return textForReplace;
            }

            var linesToInsert = textToInsert.Split(Environment.NewLine);
            var linesPrefix = string.Empty;
            if (linesToInsert.Length > 1)
            {
                var lastNewLineIndex = textForReplace.LastIndexOf(Environment.NewLine, firstMatchIndex, firstMatchIndex, StringComparison.Ordinal);
                {
                    linesPrefix = textForReplace.Substring(lastNewLineIndex + Environment.NewLine.Length, (firstMatchIndex - lastNewLineIndex) - Environment.NewLine.Length);
                }
            }

            if (!string.IsNullOrEmpty(linesPrefix))
            {
                var firstLine = linesToInsert.Take(1);
                var nextLines = linesToInsert.Skip(1).Select(x => linesPrefix + x);

                linesToInsert = firstLine.Concat(nextLines).ToArray();
            }

            textToInsert = string.Join(Environment.NewLine, linesToInsert);

            return textForReplace.Replace(textToFind, textToInsert);
        }

        private ITemplateResolver GetCustomResolver(string template)
        {
            if (template.StartsWith('$'))
            {
                template = template.TrimStart('$');
                var endIndex = template.IndexOf('(');
                var translatorName = template.Substring(0, endIndex);

                _templateTranslators.TryGetValue(translatorName, out var templateTranslator);
                return templateTranslator;
            }
            else
            {
                return ValueResolver;
            }
        }

        private string[] GetResolverParameters(string argument)
        {
            if (!argument.StartsWith('$'))
            {
                return new[] { argument };
            }

            var startIndex = argument.IndexOf('(') + 1;
            var endIndex = argument.IndexOf(')', startIndex) - 1;
            var lastIndex = argument.Length - 1;

            var param1 = argument.Substring(startIndex, endIndex - startIndex + 1);

            var param2 = string.Empty;
            var pointIndex = endIndex + 2;

            if (lastIndex > pointIndex && argument[pointIndex] == '.')
            {
                param2 = argument.Substring(pointIndex + 1);
            }

            return string.IsNullOrEmpty(param2)
                ? new[] { param1 }
                : new[] { param1, param2 };
        }

        private IEnumerable<string> ExtractTemplateArguments(string text)
        {
            var template = new StringBuilder();
            var bracketsCount = 0;

            for (var index = 0; index < text.Length; index++)
            {
                var ch = text[index];
                if (ch == '{' && index != 0 && text[index - 1] == '{')
                {
                    bracketsCount++;
                    continue;
                }

                if (ch == '}' && index < text.Length - 1 && text[index + 1] == '}')
                {
                    bracketsCount--;
                }

                if (bracketsCount == 0 && template.Length > 0)
                {
                    yield return template.ToString();
                    template.Clear();
                }

                if (bracketsCount == 0)
                {
                    continue;
                }

                template.Append(ch);
            }
        }
    }
}
