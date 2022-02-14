namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using System;
    using Catel.Data;

    public class ItemRouting
    {
        public string ItemID { get; set; }
        public string Location { get; set; }
        public string ProductionMethod { get; set; }
        public string Resource { get; set; }
        public int? Priority { get; set; }
        public int? StepNumber { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? DiscontinueDate { get; set; }
        public int? MinimumQuantity { get; set; }
        public int IncrementalQuantity { get; set; }
        public int BatchSizeRaw { get; set; }
        public int BatchSize => BatchSizeRaw != 0 ? BatchSizeRaw : IncrementalQuantity;
        public string BillOfMaterialNumber { get; set; }
        public DateTime? NoNewSupplyDate { get; set; }
        public int? NextStepTiming { get; set; }
        public double? ProductionRate { get; set; }
        public int? ProductionDuration { get; set; }
        public string ProductionRateCalendar { get; set; }
        public string ProductionVersion { get; set; }


        public override string ToString()
        {
            return $"{Resource} {ProductionVersion}";
        }
    }
}
