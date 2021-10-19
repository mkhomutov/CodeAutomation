namespace CodeAutomationConsole
{
    public class CsvDetails
    {
        public CsvDetails(string field)
        {
            Field = field;
            Alias = field;
        }

        public CsvDetails(string field, string type) : this(field)
        {
            Type = type;
        }

        public CsvDetails() { }

        public string Field { get; set; }
        public string Alias { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
    }
}
