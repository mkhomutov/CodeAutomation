namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;

    public class ParseCSV
    {        
        private readonly char[] _delimeters = new char[] { ',', ';', '\t', ' ', '|', ':' };
        private readonly string _firstLine, _secondLine;
        private readonly IEnumerable<CsvField> _fields;

        public ParseCSV(string path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                _firstLine = sr.ReadLine();
                _secondLine = sr.ReadLine();
            }

            char delimeter = DetectDelimeter(_firstLine, _secondLine);

            _fields = _firstLine.Split(delimeter).Select(x => new CsvField(x));

            foreach (var field in _firstLine.Split(delimeter))
            {
                _fields.Append(new CsvField(field));
            }
        }

        public IEnumerable<CsvField> CsvFields
        {
            get => _fields;
        }

        private char DetectDelimeter(string firstLine, string secondLine)
        {
            
            foreach (var d in _delimeters)
            {
                var count = _firstLine.Count(x => x == d);
                if (count > 0 && count == _secondLine.Count(x => x == d) ) return d;
            }
            return ',';
        }
    }
}
