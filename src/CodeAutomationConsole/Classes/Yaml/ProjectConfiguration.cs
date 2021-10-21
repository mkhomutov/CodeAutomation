namespace CodeAutomationConsole
{
    using System.Collections.Generic;

    public class ProjectConfiguration
    {
        public string CsvImportPath { get; set; }
        public string CodeExportPath { get; set; }
        public string NameSpace { get; set; }

        public List<CsvListMember> CsvList { get; set; }
    }
}
