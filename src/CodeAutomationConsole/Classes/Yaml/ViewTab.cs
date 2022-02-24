namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.Linq;

    public class ViewTab
    {
        public ViewTab()
        {
            Content = new List<Content>();
        }

        public ViewTab(string className, string fileName) : this()
        {
            Title = className;

            RelatedClassName = className;

            RelatedFileName = fileName;
        }

        public string Title { get; set; }

        public string RelatedClassName { get; set; }
        public string RelatedFileName { get; set; }

        public List<Content> Content { get; set; }

        public DataGrid GetDataGrid()
        {
            return Content.Select(x => x.DataGrid).FirstOrDefault();
        }

        public Dictionary<object, object> ToObject()
        {
            return new Dictionary<object, object>
            {
                { "title", Title },
                { "relatedClassName", RelatedClassName },
                { "relatedFileName", RelatedFileName },
                { "content", Content.Select(con => con.ToObject()).ToList() }
            };
        }

    }
}
