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

        public ViewTab(string className, string from) : this()
        {
            Title = className;

            RelatedClassName = className;

            FromCsv = from;
        }

        public string Title { get; set; }

        public string RelatedClassName { get; set; }
        public string FromCsv { get; set; }

        public List<Content> Content { get; set; }

        public DataGrid GetDataGrid()
        {
            return Content.Select(x => x.DataGrid).FirstOrDefault();
        }
    }
}
