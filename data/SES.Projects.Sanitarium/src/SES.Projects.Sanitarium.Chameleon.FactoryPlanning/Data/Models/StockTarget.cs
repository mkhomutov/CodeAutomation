

namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel.Services;

    public class StockTarget
    {
        public string Location { get; set; }
        public string ItemId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartOfWeekDate { get; set; }
        public double SafetyStock { get; set; }
        public double AboveTarget { get; set; }
        public double BelowTarget { get; set; }

        public bool IsValid()
        {
            return SafetyStock != 0 || AboveTarget != 0 || BelowTarget != 0;
        }

        public override string ToString()
        {
            return $"{Location} {ItemId} {StartDate} {StartOfWeekDate} {SafetyStock} {AboveTarget} {BelowTarget}";
        }
    }
}
