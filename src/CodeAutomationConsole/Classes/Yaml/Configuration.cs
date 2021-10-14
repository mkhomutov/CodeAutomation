namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class Configuration
    {
        public string CsvImportPath { get; set; }
        public string CodeExportPath { get; set; }
        public string Contractor { get; set; }
        public string ProjectName { get; set; }
        public string NameSpace { get; set; }

        public List<CsvListMember> CsvList { get; set; }
    }
}
