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

        public ParseType(string[] text)
        {
            _text = text;
        }

        public string GetTypes()
        {
            var isDateTime = IsDateTime(_text);

            if (IsInt(_text) == 1)
            {
                return "int";
            }

            if (IsBool(_text) == 1)
            {
                return "bool";
            }

            if (isDateTime == 1)
            {
                return "DateTime";
            }

            if (IsFloat(_text) == 1) { return "float"; }

            return "string";
        }

        private float IsInt(string[] text)
        {
            int errCounter = text.Length;

            foreach (var t in text)
            {
                try
                {
                    var i = int.Parse(t);
                }
                catch { errCounter--; }
            }

            return errCounter / text.Length;
        }

        private float IsFloat(string[] text)
        {
            int errCounter = text.Length;

            var provider = new System.Globalization.CultureInfo("en-AU");

            foreach (var t in text)
            {
                try
                {
                    var i = Single.Parse(t, System.Globalization.NumberStyles.Float, provider);
                }
                catch { errCounter--; }
            }

            return errCounter / text.Length;
        }


        private float IsBool(string[] text)
        {
            int errCounter = text.Length;

            var provider = new System.Globalization.CultureInfo("en-AU");

            foreach (var t in text)
            {
                try
                {
                    var i = bool.Parse(t);
                }
                catch { errCounter--; }
            }

            return errCounter / text.Length;
        }

        private float IsDateTime(string[] text)
        {
            int errCounter = text.Length;

            var provider = new System.Globalization.CultureInfo("en-EN");

            foreach (var t in text)
            {
                try
                {
                    var i = DateTime.Parse(t);
                }
                catch { errCounter--; }
            }

            return errCounter / text.Length;
        }
    }
}
