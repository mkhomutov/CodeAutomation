namespace CodeAutomationConsole
{
    using System;
    using System.IO;
    using System.Linq;

    public static class StringExtension
    {
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
    }
}
