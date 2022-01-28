using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CodeAutomationConsole;

public class ReflectionTemplateTranslator : ITemplateTranslator
{
    private readonly Dictionary<string, Dictionary<Type, MethodInfo>> _gettersCache = new Dictionary<string, Dictionary<Type, MethodInfo>>();

    public IReadOnlyCollection<TranslationResult> Translate(TranslationContext translationContext)
    {
        var result = new List<TranslationResult>();

        var text = translationContext.Text;
        var context = translationContext.Context;
        var rootContext = translationContext.RootContext;

        if (context is null)
        {
            return result;
        }

        var dotIndex = text.IndexOf('.');
        var propertyName = text;
        var childPropertyName = string.Empty;

        var contextDictionary = ToDictionary(context);
        if (contextDictionary is not null)
        {
            result.AddRange(GetValueFromDictionary(contextDictionary, propertyName));
            if (result.Any())
            {
                return result;
            }
        }

        if (dotIndex > 0)
        {
            propertyName = text.Substring(0, dotIndex);
            childPropertyName = text.Substring(dotIndex + 1);
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
            return Translate(new TranslationContext
            {
                Context = rootContext,
                Text = text
            });
        }

        var value = getter.Invoke(context, null);
        var dictionary = ToDictionary(value);
        if (dictionary is not null)
        {
            return GetValueFromDictionary(dictionary, childPropertyName);
        }

        if (value is IEnumerable enumerableValue and not string)
        {
            foreach (var item in enumerableValue)
            {
                foreach (var childValue in GetChildValues(item, childPropertyName))
                {
                    result.Add( new TranslationResult
                    {
                        Context = childValue.Context ?? context,
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
                Context = childValue.Context ?? context,
                TranslatedText = childValue.TranslatedText
            });
        }

        return result;
    }

    private IDictionary<object, object> ToDictionary(object obj)
    {
        if (obj is List<object> list && list.All(x => x is IDictionary<object, object>))
        {
            return list.OfType<IDictionary<object, object>>().SelectMany(x => x).ToDictionary(x => x.Key, x => x.Value);
        }

        if (obj is IDictionary<object, object> dictionary)
        {
            return dictionary;
        }

        return null;
    }

    private IReadOnlyCollection<TranslationResult> GetValueFromDictionary(IDictionary<object, object> dictionary, string propertyName)
    {
        var result = new List<TranslationResult>();

        if (dictionary is null)
        {
            return result;
        }

        var dotIndex = propertyName.IndexOf('.');
        var childPropertyName = string.Empty;

        if (dotIndex > 0)
        {
            childPropertyName = propertyName.Substring(dotIndex + 1);
            propertyName = propertyName.Substring(0, dotIndex);
        }

        if(dictionary.TryGetValue(propertyName, out var value))
        {
            if (value is string strChildValue)
            {
                result.Add( new TranslationResult
                {
                    Context = dictionary,
                    TranslatedText = strChildValue
                });

                return result;
            }

            if (value is IEnumerable enumerableValue and not string)
            {
                foreach (var item in enumerableValue)
                {
                    foreach (var childValue in GetChildValues(item, childPropertyName))
                    {
                        result.Add( new TranslationResult
                        {
                            Context = childValue.Context ?? dictionary,
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
                    Context = childValue.Context ?? dictionary,
                    TranslatedText = childValue.TranslatedText
                });
            }
        }

        return result;
    }

    private IEnumerable<TranslationResult> GetChildValues(object item, string propertyName)
    {
        if (item is string stringValue)
        {
            yield return new TranslationResult
            {
                Context = null,
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