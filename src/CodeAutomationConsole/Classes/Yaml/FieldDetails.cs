using System.Collections.Generic;

namespace CodeAutomationConsole
{
    public class FieldDetails
    {
        public FieldDetails(string name, string alias = null, string type = null, string def = null)
        {
            Name = name;
            Alias = alias;
            Type = type;
            Default = def;
        }

        public FieldDetails(Dictionary<object, object> obj)
        {
            if(obj.ContainsKey("name"))
            {
                Name = obj["name"].ToString();
            }

            if (obj.ContainsKey("alias"))
            {
                Alias = obj["alias"].ToString();
            }

            if (obj.ContainsKey("type"))
            {
                Type = obj["type"].ToString();
            }

            if (obj.ContainsKey("def"))
            {
                Default = obj["def"].ToString();
            }
        }

        public FieldDetails() { }

        public string Name { get; set; }
        public string Alias { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }

        public object ToObject()
        {
            var obj = new Dictionary<object, object>
            {
                { "name", Name },
                { "alias", Alias },
                { "type", Type },
                { "default", Default }
            };

            return obj;
        }
    }
}
