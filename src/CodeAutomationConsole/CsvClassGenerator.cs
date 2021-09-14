namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class CsvClassGenerator
    {
        private readonly String _nameSpace;
        private readonly String _className;
        private readonly String _fileName;
        private readonly String[] _fields;
        private readonly String[] _properties;

        public CsvClassGenerator(string nameSpace, string path)
        {
            _nameSpace = nameSpace;
            _className = path.Split('\\').Last().Split('.').First();
            _fileName = _className + ".cs";
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line = sr.ReadLine();
                _properties = line.Split(',').Select(x => FirstLetterDown(x)).ToArray();
                _fields = _properties.Select(x => "_" + x).ToArray();
            }
        }

        public string GenerateClassCode()
        {
            string ConstructorTitle = _className + "(" + _properties.Select(x => "string " + x).ToArray().Aggregate((x, y) => x + ", " + y) + ")";
            string ConstructorAssignments = _properties.Select(x => $"_{x} = {x};").Aggregate((x, y) => JoinLines(x, y, 3));
            string Fields = _properties.Select(x => $"private readonly string _{x};").Aggregate((x, y) => JoinLines(x, y, 2));
            string Properties = _properties.
                Select(x => $"public string {FirstLetterUp(x)}\n{{\n\tget => _{x};\n}}".Split('\n').Aggregate((x, y) => JoinLines(x, y, 2))).
                Aggregate((x, y) => JoinLines(x, y, 2));

            string Code =  @$"namespace {_nameSpace}
{{
    using System;

    public class {_className}
    {{
        // Fields
        {Fields}

        // Constructor
        public {ConstructorTitle}
        {{
            {ConstructorAssignments}
        }}

        // Properties
        {Properties}
    }}
}}
";
            return Code;
        }

        private string FirstLetterDown(string field) => field[0].ToString().ToLower() + String.Join("", field.Skip(1));

        private string FirstLetterUp(string field) => field[0].ToString().ToUpper() + String.Join("", field.Skip(1));

        private string JoinLines(string line1, string line2, int tabs) => line1 + "\r\n" + string.Concat(Enumerable.Repeat("\t", tabs)) + line2;

    }
}
