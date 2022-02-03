using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutomationConsole
{
    public class MainTemplateTranslator : ITemplateTranslator
    {
        private readonly ITemplateTranslator _reflectionTemplateTranslator = new GetConfigValueTemplateTranslator();

        public IReadOnlyCollection<TranslationResult> Translate(TranslationContext translationContext)
        {
            var rootContext = translationContext.RootContext;
            var context = translationContext.Context;
            var template = translationContext.Text;

            var dictionary = new Dictionary<string, IReadOnlyCollection<TranslationResult>>();

            // #[param]
            // #[param.name]
            // #[$Code(@param.name.name)] generate code

            var arguments = ExtractTemplateArguments(template);
            foreach (var argument in arguments)
            {
                var trimmedArgument = argument.Trim('[', ']');
                var parser = GetCustomParser(trimmedArgument);
                var parameter = GetParserParameter(trimmedArgument);

                var customContext = new TranslationContext
                {
                    RootContext = rootContext,
                    Context = context,
                    Text = parameter
                };

                dictionary["#" + argument] = parser.Translate(customContext).ToList();
            }

            var finalResult = new List<TranslationResult>
            {
                new TranslationResult
                {
                    TranslatedText = template,
                    Context = context
                }
            };

            foreach (var keyValuePair in dictionary)
            {
                var key = keyValuePair.Key;

                var preResults = finalResult.ToList();
                finalResult.Clear();

                foreach (var preResult in preResults)
                {
                    foreach (var value in keyValuePair.Value)
                    {
                        var result = new TranslationResult()
                        {
                            TranslatedText = preResult.TranslatedText.Replace(key, value.TranslatedText),
                            Context = value.Context
                        };

                        finalResult.Add(result);
                    }
                }
            }

            return finalResult;
        }

        private ITemplateTranslator GetCustomParser(string template)
        {
            if (template.StartsWith('$'))
            {
                throw new NotImplementedException();
            }
            else
            {
                return _reflectionTemplateTranslator;
            }
        }

        private string GetParserParameter(string template)
        {
            if (template.StartsWith('$'))
            {
                throw new NotImplementedException();
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

            foreach (var ch in text)
            {
                // TODO work with #, which means start of the template
                if (ch == '[')
                {
                    bracketsCount++;
                    template.Append(ch);
                    continue;
                }

                if (ch == ']')
                {
                    bracketsCount--;
                    template.Append(ch);
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
