namespace CodeAutomationConsole
{
    using System.Linq;
    using System.Collections.Generic;

    public class CsvClassGenerator
    {
        private readonly string _nameSpace;
        private readonly string _className;
        private readonly IEnumerable<CsvField> _fields;

        public CsvClassGenerator(string nameSpace, string path)
        {
            _nameSpace = nameSpace;
            _className = path.Split('\\').LastOrDefault().Split('.').FirstOrDefault();
            ParseCSV csv = new ParseCSV(path);
            _fields = csv.CsvFields;
        }

        public string GenerateClassCode()
        {
            string constructorTitle = _className + "(" + _fields.Select(x => "string " + x.ConstructorInput).Aggregate((x, y) => x + ", " + y) + ")";
            string constructorAssignments = JoinWithTabs(_fields.Select(x => $"{x.Field} = {x.ConstructorInput};"), 3);
            string fields = JoinWithTabs(_fields.Select(x => $"private readonly string {x.Field};"), 2);
            string properties = JoinWithTabs(_fields.Select(x => JoinWithTabs($"public string {x.Property}\n{{\n\tget => {x.Field};\n}}".Split('\n'), 2)), 2);
                        
            string code =  @$"namespace {_nameSpace}
{{
    using System;

    public class {_className}
    {{
        // Fields
        {fields}

        // Constructor
        public {constructorTitle}
        {{
            {constructorAssignments}
        }}

        // Properties
        {properties}
    }}
}}
";
            return code;
        }

        private string JoinWithTabs(IEnumerable<string> lines, int tabs) => lines.Aggregate((x, y) => x + "\r\n" + string.Concat(Enumerable.Repeat("\t", tabs)) + y);
    }
}
