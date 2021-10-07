namespace CodeAutomationConsole
{
    using System.Linq;
    using System.Collections.Generic;

    public class ClassGenerator
    {
        private readonly string _nameSpace;
        private readonly string _className;

        private readonly IEnumerable<CsvField> _fields;

        public ClassGenerator(string nameSpace, string path)
        {
            var csv = new ParseCSV(path);

            _nameSpace = nameSpace;
            _className = path.Split('\\').LastOrDefault().Split('.').FirstOrDefault();
            _fields = csv.CsvFields;
        }

        public string GenerateClassCode()
        {
            var properties = JoinWithTabs(_fields.Select(x => $"public string {x.Property} {{ get; set; }}"), 2);

            var code =  @$"namespace {_nameSpace}
{{
    using System;

    public class {_className}
    {{
        #region Properties
        {properties}
        #endregion
    }}
}}
";
            return code;
        }

        private string JoinWithTabs(IEnumerable<string> lines, int tabs) => lines.Aggregate((x, y) => x + "\r\n" + string.Concat(Enumerable.Repeat("\t", tabs)) + y);
    }
}
