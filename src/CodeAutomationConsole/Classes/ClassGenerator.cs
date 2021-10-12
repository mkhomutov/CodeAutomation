namespace CodeAutomationConsole
{
    using System.Linq;
    using System.Collections.Generic;

    public class ClassGenerator
    {
        private readonly string _nameSpace;
        private readonly string _className;
        private readonly string _properties;

        public ClassGenerator(string nameSpace, string path)
        {
            var csv = new ParseCSV(path);

            _nameSpace = nameSpace;
            _className = path.Split('\\').LastOrDefault().Split('.').FirstOrDefault();
            _properties = JoinWithTabs(csv.Headers.Select(x => $"public string {x} {{ get; set; }}"), 2);
        }

        public ClassGenerator(string nameSpace, string path, CsvList settings)
        {
            var csv = new ParseCSV(path);

            _nameSpace = nameSpace;
            _className = settings.ClassName is null ? path.Split('\\').LastOrDefault().Split('.').FirstOrDefault() : settings.ClassName;

            _properties = JoinWithTabs(csv.Headers.Select(x =>
            {
                var fieldDetails = settings.GetDetails(x);

                var field = fieldDetails is null ? x : fieldDetails.Alias is null ? x : fieldDetails.Alias;

                return $"public string {field} {{ get; set; }}";
            }), 2);
        }

        public string GenerateClassCode()
        {
            var code =  @$"namespace {_nameSpace}
{{
    using System;

    public class {_className}
    {{
        #region Properties
        {_properties}
        #endregion
    }}
}}
";
            return code;
        }

        private string JoinWithTabs(IEnumerable<string> lines, int tabs) => lines.Aggregate((x, y) => x + "\r\n" + string.Concat(Enumerable.Repeat("\t", tabs)) + y);
    }
}
