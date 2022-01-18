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

        public string Title { get; set; }

        public string RelatedPropertyName { get; set;}

        public string FromCsvField { get; set; }
    }
}
