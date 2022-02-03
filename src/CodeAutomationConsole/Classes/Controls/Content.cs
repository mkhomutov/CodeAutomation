namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Content
    {
        public Content() { }

        public DataGrid DataGrid { get; set; }

        public Dictionary<object, object> ToObject()
        {
            return new Dictionary<object, object>
            {
                { "dataGrid", DataGrid.ToObject() }
            };
        }
    }
}
