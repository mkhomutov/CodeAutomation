namespace CodeAutomationConsole
{
    public class CsvDetails
    {
        public CsvDetails(string field, string alias = null, string type = null, string def = null)
        {
            Field = field;
            Alias = alias;
            Type = type;
            Default = def;
        }

        public CsvDetails() { }

        public string Field { get; set; }
        public string Alias { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
    }
}
