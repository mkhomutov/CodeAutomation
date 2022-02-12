using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scriban.Runtime;

namespace CodeAutomationConsole
{
    public static class ObjectExtension
    {
        public static Dictionary<object, object> ToObjectDictionary(this object obj)
        {
            return (Dictionary<object, object>)(IEnumerable<KeyValuePair<object, object>>)obj;
        }

        public static object FixTypes(this object obj)
        {
            switch (obj)
            {
                case string @string:
                {
                    if (long.TryParse(@string, out var lng))
                        return lng;
                    if (double.TryParse(@string, out var dbl))
                        return dbl;
                    if (bool.TryParse(@string, out var b))
                        return b;
                    return @string;
                }

                case Dictionary<object, object> dictionary:
                    var scriptObject = new ScriptObject();
                    foreach (var (key, value) in dictionary)
                    {
                        scriptObject[key.ToString()] = value.FixTypes();
                    }

                    return scriptObject;

                case IEnumerable<object> enumerable:
                    return enumerable.Select(x => x.FixTypes()).ToArray();

                default:
                    return obj;
            }
        }
    }
}
