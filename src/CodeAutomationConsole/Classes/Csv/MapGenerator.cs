namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MapGenerator
    {
        private readonly string _nameSpace;
        private readonly string _className;
        private readonly string _mappings;

        public MapGenerator(string nameSpace, string path)
        {
            var csv = new ParseCSV(path);

            _nameSpace = nameSpace;
            _className = Path.GetFileNameWithoutExtension(path);
            _mappings = JoinWithTabs(csv.Headers.Select(x => $"Map(x => x.{x}).Name(\"{x}\");"), 3);
        }

        public MapGenerator(string nameSpace, string path, CsvListMember settings)
        {
            var csv = new ParseCSV(path);

            _nameSpace = nameSpace;
            _className = settings.ClassName ?? Path.GetFileNameWithoutExtension(path);

            _mappings = JoinWithTabs(csv.Headers.Select(x =>
            {
                var fieldDetails = settings.GetDetails(x);

                var fieldAlias = fieldDetails?.Alias ?? x;
                var fieldType = fieldDetails?.Type is null ? "" : $".As{Capitalize(fieldDetails.Type)}()";
                var fieldDefault = fieldDetails?.Default is null ? "" : $".Default({fieldDetails.Default})";

                return $"Map(x => x.{fieldAlias}).Name(\"{x}\"){fieldType}{fieldDefault};";
            }), 3);
        }

        public string GenerateMapCode()
        {
            string code = @$"namespace {_nameSpace}
{{
    using Orc.Csv;

    public class {_className} : ClassMapBase<{_className}>
    {{
        #region Constructors
        public {_className}()
        {{
            {_mappings}
        }}
        #endregion
    }}
}}
";
            return code;
        }

        private string JoinWithTabs(IEnumerable<string> lines, int tabs) => lines.Aggregate((x, y) => x + "\r\n" + string.Concat(Enumerable.Repeat("\t", tabs)) + y);

        private string Capitalize(string text) => text[0].ToString().ToUpper() + text.Substring(1, text.Length - 1);
    }
}
