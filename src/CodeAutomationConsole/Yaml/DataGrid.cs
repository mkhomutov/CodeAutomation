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

        public DataGrid(List<FieldDetails> csvDetails)
        {
            Columns = new List<DataGridColumn>();

            csvDetails.ForEach(x =>
            {
                Columns.Add(new DataGridColumn(x.Alias, x.Name));
            });
        }

        public DataGrid(List<object> csvDetails)
        {
            Columns = new List<DataGridColumn>();

            csvDetails.ForEach(x =>
            {
                var details = (Dictionary<object, object>)x;

                var className = details.ContainsKey("alias") ? details["alias"] ?? details["name"] : details["name"];
                var name = details["name"];

                Columns.Add(new DataGridColumn(className.ToString(), name.ToString()));
            });
        }

        public List<DataGridColumn> Columns { get; set; }

        public Dictionary<object, object> ToObject()
        {
            return new Dictionary<object, object>
            {
                { "columns", Columns.Select(column => column.ToObject()).ToList() }
            };
        }
    }
}
