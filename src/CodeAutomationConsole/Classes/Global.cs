namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Global
    {
        public static string Namespace { get; set; }

        public static string Path { get; set; }

        public static List<CsvListMember> CsvList { get; set; }

        public static LoadProjectConfiguration Config { get; set; }
    }
}
