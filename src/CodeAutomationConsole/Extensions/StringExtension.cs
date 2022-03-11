using System.Globalization;
using Humanizer;

namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public static class StringExtension
    {
        public static Dictionary<string, HashSet<string>> PropertyNamesByClass =
            new Dictionary<string, HashSet<string>>();

        public static char[] Digits = new [] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public static string WildCardToRegular(this string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        public static string ToValidPropertyName(this string txt, string className)
        {
            txt = txt.TrimStart(Digits);

            var charArray = string.Equals(txt, txt.ToUpper()) ? txt.ToLower().ToCharArray() : txt.ToCharArray();

            var flag = true;

            for (var i = 0; i < charArray.Length; i++)
            {
                charArray[i] = flag ? char.ToUpper(charArray[i]) : charArray[i];

                flag = !char.IsLetterOrDigit(charArray[i]);
            }

            var propertyName = string.Concat(charArray.Where(char.IsLetterOrDigit));

            propertyName = ToUniquePropertyName(className, propertyName);

            return propertyName;
        }

        private static string ToUniquePropertyName(string className, string propertyName)
        {
            if (!PropertyNamesByClass.TryGetValue(className, out var properties))
            {
                properties = new HashSet<string>();
                PropertyNamesByClass[className] = properties;
            }

            var resultName = propertyName;
            var count = 1;
            while (properties.Contains(resultName))
            {
                resultName = propertyName + (++count);
            }

            properties.Add(resultName);

            return resultName;
        }
        
        public static object ImportFromYaml(this string path)
        {
            if (!File.Exists(path))
            {
                return new Dictionary<string, string>
                {
                    { "import", path },
                    { "error", "File doesn't exist" }
                };
            }

            using var sr = new StreamReader(path, Encoding.Default);
            var yaml = sr.ReadToEnd();

            var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            
            return deserializer.Deserialize<object>(yaml);
        }

        public static Type ResolveType(this IReadOnlyCollection<string> stringValues, CultureInfo cultureInfo)
        {
            if (IsInt(stringValues))
            {
                return typeof(int);
            }

            if (IsBool(stringValues))
            {
                return typeof(bool);
            }

            if (IsDouble(stringValues, cultureInfo))
            {
                return typeof(double);
            }

            if (IsDateTime(stringValues, cultureInfo))
            {
                return typeof(DateTime);
            }
            
            return typeof(string);
        }

        private static bool IsInt(IReadOnlyCollection<string> stringValues)
        {
            return stringValues.All(x => int.TryParse(x, out _));
        }

        private static bool IsDouble(IReadOnlyCollection<string> stringValues, CultureInfo cultureInfo)
        {
            var style = System.Globalization.NumberStyles.Float;

            return stringValues.All(x => double.TryParse(x, style, cultureInfo, out _));
        }
        
        private static bool IsBool(IReadOnlyCollection<string> stringValues)
        {
            return stringValues.All(x => bool.TryParse(x, out _));
        }

        private static bool IsDateTime(IReadOnlyCollection<string> stringValues, CultureInfo cultureInfo)
        {
            var style = System.Globalization.DateTimeStyles.None;

            return stringValues.All(x => DateTime.TryParse(x, cultureInfo, style, out _));
        }
    }
}
