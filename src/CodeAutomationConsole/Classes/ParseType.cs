namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ParseType
    {
        private readonly string[] _text;
        private readonly string _type;

        public ParseType(string[] text)
        {
            _text = text;

            if (IsInt()) { _type = "int"; }
            if (IsBool()) { _type = "bool"; }
            if (IsDouble()) { _type = "double"; }
            if (IsDateTime()) { _type = "DateTime"; }
        }

        public string Type { get => _type; }

        private bool IsInt()
        {
            foreach (var t in _text)
            {
                if (!int.TryParse(t, out int x)) { return false; }
            }

            return true;
        }

        private bool IsDouble()
        {
            var provider = new System.Globalization.CultureInfo("en-AU");
            var style = System.Globalization.NumberStyles.Float;

            foreach (var t in _text)
            {
                if (!double.TryParse(t, style, provider, out var i))
                {
                    return false;
                }
            }

            return true;
        }


        private bool IsBool()
        {
            foreach (var t in _text)
            {
                if (!bool.TryParse(t, out var i)) { return false; }
            }

            return true;
        }

        private bool IsDateTime()
        {
            var provider = new System.Globalization.CultureInfo("en-US");

            var style = System.Globalization.DateTimeStyles.None;

            foreach (var t in _text)
            {
                if (!DateTime.TryParse(t, provider, style, out var dt)) { return false; }
            }

            return true;
        }
    }
}
