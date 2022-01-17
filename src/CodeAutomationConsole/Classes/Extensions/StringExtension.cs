namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtension
    {
        public static string Capitalize(this string txt)
        {
            return txt[0].ToString().ToUpper() + txt.Substring(1, txt.Length - 1);
        }

        public static string ToValidPopertyName(this string txt)
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

            return string.Concat(charArray.Where(x => char.IsLetterOrDigit(x)));
        }

        public static void SaveToFile(this string txt, string fileName)
        {
            var dir = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var fstream = new FileStream(fileName, FileMode.Create))
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(txt);
                fstream.Write(array, 0, array.Length);
                Console.WriteLine($"Generated: {fileName}");
            }
        }

        // Add copright at the begin of text
        public static string AddCopyright(this string txt, string filename)
        {
            string copyright = @$"// --------------------------------------------------------------------------------------------------------------------
// <copyright file=""{filename}"" company=""WildGums"">
//   Copyright (c) 2008 - 2021 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
";
            return copyright + txt;
        }

        // Format XElement.ToString() to readable XAML
        public static string FormatXaml(this string txt)
        {
            string t;

            var regex = new Regex(@"([\w:]+?="".*?""|^\s*<.*?\s|/?>$)");

            var strBuilder = new StringBuilder();

            using (var strReader = new StringReader(txt))
            {
                while ((t = strReader.ReadLine()) is not null)
                {
                    t = t.Replace("  ", "    ");

                    var matches = regex.Matches(t);

                    if (matches.Count > 3)
                    {
                        var margin = string.Concat(Enumerable.Repeat(" ", matches[0].Length));

                        strBuilder.AppendLine(matches[0].Value + matches[1].Value);
                        foreach (var str in matches.Skip(2).SkipLast(2).Select(x => x.Value))
                        {
                            strBuilder.AppendLine(margin + str);
                        }

                        strBuilder.AppendLine(margin + matches[^2].Value + matches[^1].Value);
                    }
                    else
                    {
                        strBuilder.AppendLine(t);
                    }
                }
            }

            return strBuilder.ToString();
        }

        public static string RemoveXmlns(this string txt)
        {
            var pattern = @"\s*xmlns:?\w*="".*?""";

            var regex = new Regex(pattern);

            return regex.Replace(txt, "");
        }

        public static string Tabulate(this string txt, int count)
        {
            string t;

            var strBuilder = new StringBuilder();

            var tabs = string.Concat(Enumerable.Repeat("\t", count));

            using (var strReader = new StringReader(txt))
            {
                while ((t = strReader.ReadLine()) is not null)
                {
                    strBuilder.AppendLine($"{tabs}{t}");
                }
            }

            return strBuilder.ToString();
        }
    }
}
