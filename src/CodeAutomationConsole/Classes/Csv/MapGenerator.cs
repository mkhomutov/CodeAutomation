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
            _mappings = csv.Headers.Select(x => $"Map(x => x.{x}).Name(\"{x}\");").JoinWithTabs(3);
        }

        public MapGenerator(string nameSpace, string path, CsvListMember settings)
        {
            var csv = new ParseCSV(path);

            _nameSpace = nameSpace;
            _className = settings.ClassName ?? Path.GetFileNameWithoutExtension(path);

            _mappings = csv.Headers.Select(x =>
            {
                var fieldDetails = settings.GetDetails(x);

                var fieldAlias = fieldDetails?.Alias ?? x;
                var fieldType = fieldDetails?.Type is null ? "" : $".As{fieldDetails.Type.Capitalize()}()";
                var fieldDefault = fieldDetails?.Default is null ? "" : $".Default({fieldDetails.Default})";

                if (fieldAlias.Equals(_className)) { fieldAlias += "Property"; }

                return $"Map(x => x.{fieldAlias}).Name(\"{x}\"){fieldType}{fieldDefault};";
            }).JoinWithTabs(3);
        }

        public string GenerateMapCode()
        {
            string code = @$"using Orc.Csv;

namespace {_nameSpace}.Data.Models.Maps;

public sealed class {_className}Map : ClassMapBase<{_className}>
{{
    #region Constructors
    public {_className}Map()
    {{
        {_mappings}
    }}
    #endregion
}}
";
            return code;
        }
    }
}
