namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;

    public class CsvListMember
    {
        public string Name { get; set; }
        public string ClassName { get; set; }

        public List<CsvDetails> Details { get; set; }

        public CsvDetails GetDetails(string name)
        {
            return Details.Find(x => string.Equals(x.Field, name));
        }

        public string GenerateClass()
        {
            var properties = Details.Select(x =>
            {
                var field = x.Alias ?? x.Field;
                var type = x.Type ?? "string";

                if (field.Equals(ClassName)) { field += "Property"; }

                return $"public {type} {field} {{ get; set; }}";
            }).JoinWithTabs(2);

            var content = Template.GetByName("[DataModelClassName].cs").
                Replace("%CLASSNAME%", ClassName).
                Replace("%PROPERTIES%", properties);

            return content;
        }

        public string GenerateMap()
        {
            var mappings = Details.Select(x =>
            {
                var alias = x.Alias ?? x.Field;
                var fieldType = x.Type is null ? "" : $".As{x.Type.Capitalize()}()";
                var fieldDefault = x.Default is null ? "" : $".Default({x.Default})";

                if (alias.Equals(ClassName)) { alias += "Property"; }

                return $"Map(x => x.{alias}).Name(\"{x.Field}\"){fieldType}{fieldDefault};";
            }).JoinWithTabs(2);

            var content = Template.GetByName("[DataModelClassNameMap].cs").
                Replace("%CLASSNAME%", ClassName).
                Replace("%MAPPINGS%", mappings);

            return content;
        }
    }
}
