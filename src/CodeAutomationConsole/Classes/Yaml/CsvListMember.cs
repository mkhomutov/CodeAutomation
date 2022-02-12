namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CsvListMember
    {
        public CsvListMember() { }

        public CsvListMember(string file) : this()
        {
            var fileName = Path.GetFileNameWithoutExtension(file);

            File = fileName;
            ClassName = fileName.EndsWith('s') ? fileName.Substring(0, fileName.Length - 1) : fileName;
        }

        public string File { get; set; }
        public string ClassName { get; set; }

        public List<FieldDetails> Fields { get; set; }

        public FieldDetails GetDetails(string name)
        {
            return Fields.Find(x => string.Equals(x.Name, name));
        }

        public object ToObject()
        {
            var obj = new Dictionary<object, object>
            {
                { "file", File },
                { "className", ClassName },
                { "fields", Fields.Select(field => field.ToObject()).ToList() }
            };

            return obj;
        }

        public string GenerateClass()
        {
            var properties = Fields.Select(x =>
            {
                var field = x.Alias ?? x.Name;
                var type = x.Type ?? "string";

                if (field.Equals(ClassName)) { field += "Property"; }

                return $"public {type} {field} {{ get; set; }}";
            }).JoinWithTabs(2);

            var content = CodeTemplate.GetByName("[DataModelClassName].cs").
                Replace("%CLASSNAME%", ClassName).
                Replace("%PROPERTIES%", properties);

            return content;
        }

        public string GenerateMap()
        {
            var mappings = Fields.Select(x =>
            {
                var alias = x.Alias ?? x.Name;
                var fieldType = x.Type is null ? "" : $".As{x.Type.Capitalize()}()";
                var fieldDefault = x.Default is null ? "" : $".Default({x.Default})";

                if (alias.Equals(ClassName)) { alias += "Property"; }

                return $"Map(x => x.{alias}).Name(\"{x.Name}\"){fieldType}{fieldDefault};";
            }).JoinWithTabs(2);

            var content = CodeTemplate.GetByName("[DataModelClassNameMap].cs").
                Replace("%CLASSNAME%", ClassName).
                Replace("%MAPPINGS%", mappings);

            return content;
        }
    }
}
