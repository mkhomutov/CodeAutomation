using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CodeAutomationConsole;

public class ReflectionTemplateTranslator : ITemplateTranslator
{
    private readonly Dictionary<string, Dictionary<Type, MethodInfo>> _gettersCache = new Dictionary<string, Dictionary<Type, MethodInfo>>();

    public IReadOnlyCollection<TranslationResult> Translate(TranslationContext translationContext)
    {
        var result = new List<TranslationResult>();

        var template = translationContext.Text;
        var context = translationContext.SolutionItem?.Context ?? translationContext.Context;
        var rootContext = translationContext.SolutionItem?.GetRoot().Context ?? context;

        if (context is null)
        {
            return result;
        }

        var dotIndex = template.IndexOf('.');
        var propertyName = template;
        var childPropertyName = string.Empty;

        if (dotIndex > 0)
        {
            propertyName = template.Substring(0, dotIndex);
            childPropertyName = template.Substring(dotIndex + 1);
        }

        if(!_gettersCache.TryGetValue(propertyName, out var gettersByType))
        {
            gettersByType = new Dictionary<Type, MethodInfo>();
            _gettersCache[propertyName] = gettersByType;
        }

        var type = context.GetType();
        if (!gettersByType.TryGetValue(type, out var getter))
        {
            var propertyInfo = type.GetProperty(propertyName);
            getter = propertyInfo?.GetMethod;

            gettersByType[type] = getter;
        }

        if (getter is null)
        {
            return result;
        }

        var value = getter.Invoke(context, null);
        if (value is IEnumerable enumerableValue and not string)
        {
            foreach (var item in enumerableValue)
            {
                foreach (var childValue in GetChildValues(item, childPropertyName))
                {
                    result.Add( new TranslationResult
                    {
                        Context = item,
                        TranslatedText = childValue.TranslatedText
                    });
                }
            }

            return result;
        }

        foreach (var childValue in GetChildValues(value, childPropertyName))
        {
            result.Add(new TranslationResult
            {
                Context = value,
                TranslatedText = childValue.TranslatedText
            });
        }

        return result;
    }

    private IEnumerable<TranslationResult> GetChildValues(object item, string propertyName)
    {
        if (item is string stringValue)
        {
            yield return new TranslationResult
            {
                Context = item,
                TranslatedText = stringValue
            };
        }
        else
        {
            var context = new TranslationContext
            {
                Context = item,
                Text = propertyName
            };

            foreach (var childValue in Translate(context))
            {
                yield return childValue;
            }
        }
    }
}