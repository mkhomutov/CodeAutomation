namespace CodeAutomationConsole
{
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

        public static string GetCommandLine(this string txt)
        {
            var commandArguments = txt.Split(' ').Skip(1).ToArray();

            return string.Join(" ", commandArguments);
        }
    }
}
