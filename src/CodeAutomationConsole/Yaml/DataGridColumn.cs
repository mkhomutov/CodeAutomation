using System.Collections.Generic;

namespace CodeAutomationConsole
{
    public class DataGridColumn
    {
        public DataGridColumn() { }

        public DataGridColumn(string title, string from)
        {
            Title = title ?? from;

            RelatedPropertyName = Title;

            FromCsvField = from;
        }

        public DataGridColumn(Dictionary<object, object> obj)
        {
            Title = obj["title"].ToString();

            RelatedPropertyName = obj["relatedPropertyName"].ToString();

            FromCsvField = obj["fromCsvField"].ToString();
        }


        public string Title { get; set; }

        public string RelatedPropertyName { get; set;}

        public string FromCsvField { get; set; }

        public Dictionary<object, object> ToObject()
        {
            return new Dictionary<object, object>
            {
                { "title", Title },
                { "relatedPropertyName", RelatedPropertyName },
                { "fromCsvField", FromCsvField}
            };
        }
    }
}
