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

            Fields = new ParseCSV(file).Details;
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
    }
}
