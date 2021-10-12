namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class CsvList
    {
        public string Name { get; set; }
        public string ClassName { get; set; }

        public List<CsvDetails> Details { get; set; }

        public CsvDetails GetDetails(string name)
        {
            return Details.Find(x => x.Field == name);
        }
    }
}
