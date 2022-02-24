using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scriban.Runtime;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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

                    if (dictionary.ContainsKey("import"))
                    {
                        var import = dictionary["import"].ToString().ImportFromYaml();

                        if (import.GetType() == typeof(Dictionary<object, object>))
                        {
                            return import.FixTypes();
                        }

                        if (import.GetType() == typeof(List<object>))
                        {
                            var enumerable = (IEnumerable<object>) import;
                            return enumerable.Select(x => x.FixTypes()).ToArray();
                        }

                    }
                    else
                    {

                        foreach (var (key, value) in dictionary)
                        {
                            scriptObject[key.ToString()] = value.FixTypes();
                        }
                    }

                    return scriptObject;

                case IEnumerable<object> enumerable:
                    return enumerable.Select(x => x.FixTypes()).ToArray();

                default:
                    return obj;
            }
        }

        public static void ExportYaml(this object obj, string path)
        {
            var serializer = new SerializerBuilder().
                WithNamingConvention(CamelCaseNamingConvention.Instance).
                ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull).
                Build();

            var content = serializer.Serialize(obj);

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            File.WriteAllText(path, content);
        }

    }
}
