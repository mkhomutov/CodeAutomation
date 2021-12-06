namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class CsvListMember
    {
        public string Name { get; set; }
        public string ClassName { get; set; }

        public List<CsvDetails> Details { get; set; }

        public CsvDetails GetDetails(string name)
        {
            return Details.Find(x => string.Equals(x.Field, name));
        }
    }
}
