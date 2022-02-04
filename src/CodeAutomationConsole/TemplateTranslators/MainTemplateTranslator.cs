using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutomationConsole
{
    public class MainTemplateTranslator : TemplateTranslatorBase
    {
        private readonly Dictionary<string, ITemplateTranslator> _templateTranslators;

        public MainTemplateTranslator()
        {
            _templateTranslators = new Dictionary<string, ITemplateTranslator>()
            {
                { "Guid", new GuidTemplateTranslator() },
                { "AssemblyName", new AssemblyNameTemplateTranslator() },
                { "Namespace", new NamespaceTemplateTranslator() },
                { "MapProperties", new MapPropertiesTemplateTranslator() },
            };
        }

        public override IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
        {
            var rootContext = translationContext.RootContext;
            var context = translationContext.Context;
            var template = translationContext.Argument;

            var dictionary = new Dictionary<string, IReadOnlyCollection<SettingValue>>();

            // #[param]
            // #[param.name]
            // #[$Code(@param.name.name)] generate code

            var arguments = ExtractTemplateArguments(template);
            foreach (var argument in arguments)
            {
                var parser = GetCustomParser(argument);
                var parameter = GetParserParameter(argument);

                var customContext = new TranslationContext
                {
                    RootContext = rootContext,
                    Context = context,
                    Argument = parameter
                };

                dictionary["#[" + argument + "]#"] = parser.Translate(customContext).ToList();
            }

            var resultsCount = 1;
            foreach (var count in dictionary.Select(x => x.Value.Count))
            {
                resultsCount *= count;
            }

            if (resultsCount == 0)
            {
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
                        settingValue.Value = ((string)settingValue.Value).Replace(key, (string)item.Value);
                        settingValue.Context = item.Context;
                    }
                }
            }

            return finalResult;
        }

        private ITemplateTranslator GetCustomParser(string template)
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
                return SettingsValueTemplateTranslator;
            }
        }

        private string GetParserParameter(string template)
        {
            if (template.StartsWith('$'))
            {
                var startIndex = template.IndexOf('(') + 1;
                var endIndex = template.IndexOf(')', startIndex) - 1;
                return template.Substring(startIndex, endIndex - startIndex + 1);
            }
            else
            {
                return template;
            }
        }

        private IEnumerable<string> ExtractTemplateArguments(string text)
        {
            var template = new StringBuilder();
            var bracketsCount = 0;

            for (var index = 0; index < text.Length; index++)
            {
                var ch = text[index];
                if (ch == '[' && index != 0 && text[index - 1] == '#')
                {
                    bracketsCount++;
                    continue;
                }

                if (ch == ']' && index < text.Length - 1 && text[index + 1] == '#')
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
