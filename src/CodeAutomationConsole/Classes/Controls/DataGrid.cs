namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DataGrid
    {
        public DataGrid() { }

        public DataGrid(List<CsvDetails> csvDetails)
        {
            Columns = new List<DataGridColumn>();

            csvDetails.ForEach(x =>
            {
                Columns.Add(new DataGridColumn(x.Alias, x.Field));
            });
        }

        public List<DataGridColumn> Columns { get; set; }
    }
}
