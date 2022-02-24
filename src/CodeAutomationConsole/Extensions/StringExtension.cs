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

        public static string WildCardToRegular(this string value)
        {
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        public static string ToValidPropertyName(this string txt, string className)
        {
            var digits = new [] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            txt = txt.TrimStart(digits);

            var charArray = string.Equals(txt, txt.ToUpper()) ? txt.ToLower().ToCharArray() : txt.ToCharArray();

            bool flag = true;

            for (var i = 0; i < charArray.Length; i++)
            {
                charArray[i] = flag ? char.ToUpper(charArray[i]) : charArray[i];

                flag = !char.IsLetterOrDigit(charArray[i]);
            }

            var propertyName = string.Concat(charArray.Where(x => char.IsLetterOrDigit(x)));

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
            if (File.Exists(path))
            {
                using var sr = new StreamReader(path, Encoding.Default);
                var yaml = sr.ReadToEnd();

                var deserializer = new DeserializerBuilder().WithNamingConvention(CamelCaseNamingConvention.Instance).Build();
                var result = deserializer.Deserialize<object>(yaml);

                return result;
            }
            else
            {
                return new Dictionary<string, string>
                {
                    { "import", path },
                    { "error", "File doesn't exist" }
                };
            }

        }
    }
}
