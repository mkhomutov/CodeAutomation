
namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TransitItem
    {
        public Item Item { get; set; }
        public string Location { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime StartOfWeekDate { get; set; }
        public string ItemId { get; set; }
        public double Quantity { get; set; } // Cases
    }
}
