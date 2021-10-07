namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MapGenerator
    {
        private readonly string _nameSpace;
        private readonly string _className;

        private readonly IEnumerable<CsvField> _fields;

        public MapGenerator(string nameSpace, string path)
        {
            var csv = new ParseCSV(path);

            _nameSpace = nameSpace;
            _className = path.Split('\\').LastOrDefault().Split('.').FirstOrDefault();
            _fields = csv.CsvFields;
        }

        public string GenerateMapCode()
        {
            var mappings = JoinWithTabs(_fields.Select(x => $"Map(x => x.{x.Property});"), 3);

            var code = @$"namespace {_nameSpace}
{{
    using Orc.Csv;

    public class {_className} : ClassMapBase<{_className}>
    {{
        #region Constructors
        public {_className}()
        {{
            {mappings}
        }}
        #endregion
    }}
}}
";
            return code;
        }

        private string JoinWithTabs(IEnumerable<string> lines, int tabs) => lines.Aggregate((x, y) => x + "\r\n" + string.Concat(Enumerable.Repeat("\t", tabs)) + y);
    }
}
