using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;

namespace CodeAutomationConsole;

public class SettingsValueResolver
{
    private readonly Dictionary<string, Dictionary<Type, MethodInfo>> _gettersCache = new Dictionary<string, Dictionary<Type, MethodInfo>>();

    public IReadOnlyCollection<SettingValue> TryGetValues(object obj, string propertyPath)
    {
        if (string.IsNullOrEmpty(propertyPath))
        {
            return new[]
            {
                new SettingValue
                {
                    Context = obj,
                    Value = obj
                }
            };
        }

        var result = new List<SettingValue>();

        var properties = propertyPath.Split('.');
        var stack = new Stack<string>();
        for (var i = properties.Length -1; i >=0; i--)
        {
            var property = properties[i];
            stack.Push(property);
        }

        var lastContextValues = new HashSet<object> { obj };

        while (stack.Any())
        {
            var property = stack.Pop();

            var contextValues = lastContextValues.ToList();
            lastContextValues.Clear();

            foreach (var context in contextValues)
            {
                var listContext = new List<object>();
                if (context is IEnumerable enumerableContext)
                {
                    listContext.AddRange(enumerableContext.OfType<object>());
                }
                else
                {
                    listContext.Add(context);
                }

                var values = new List<KeyValuePair<object, object>>();

                foreach (var contextItem in listContext)
                {
                    var value = TryGetValuesUsingReflection(contextItem, property)
                                ?? TryGetValuesByKey(contextItem, property);

                    if (value is null)
                    {
                        continue;
                    }

                    if (stack.Any())
                    {
                        lastContextValues.Add(value);
                        continue;
                    }

                    if (value is IEnumerable enumerable and not string)
                    {
                        var resultContext = context is IDictionary<object, object>
                            ? context
                            : context is IEnumerable and not string
                                ? contextItem
                                : context;
                        values.AddRange(enumerable.OfType<object>().Select(x => new KeyValuePair<object, object>(resultContext, x)));
                    }
                    else
                    {
                        values.Add(new KeyValuePair<object, object>(contextItem, value));
                    }
                }

                if (values.Any())
                {
                    result.AddRange(values.Select(x => new SettingValue
                    {
                        Value = x.Value,
                        Context = values.Count > 1 ? x.Key : context
                    }));
                }
            }
        }

        return result;
    }

    private object TryGetValuesUsingReflection(object obj, string propertyName)
    {
        if(!_gettersCache.TryGetValue(propertyName, out var gettersByType))
        {
            gettersByType = new Dictionary<Type, MethodInfo>();
            _gettersCache[propertyName] = gettersByType;
        }

        var type = obj.GetType();
        if (!gettersByType.TryGetValue(type, out var getter))
        {
            var propertyInfo = type.GetProperty(propertyName);
            getter = propertyInfo?.GetMethod;

            gettersByType[type] = getter;
        }

        if (getter is null)
        {
            return null;
        }

        var value = getter.Invoke(obj, null);
        return value;
    }

    private object TryGetValuesByKey(object obj, string key)
    {
        if (obj is IDictionary<object, object> dictionary && dictionary.TryGetValue(key, out var value))
        {
            return value;
        }

        if (obj is KeyValuePair<object, object> keyValuePair && keyValuePair.Key.Equals(key))
        {
            return keyValuePair.Value;
        }

        if (obj is IEnumerable enumerable and not string)
        {
            var dictionaryValues = enumerable.OfType<IDictionary<object, object>>().Where(x => x.ContainsKey(key))
                .Select(x => x[key]).ToList();

            var keyValuePairValues = enumerable.OfType<KeyValuePair<object, object>>().Where(x => x.Key.Equals(key))
                .Select(x => x.Value).ToList();

            var result = dictionaryValues.Concat(keyValuePairValues).ToList();

            if (result.Any())
            {
                return result;
            }
        }

        return null;
    }
}