namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using System;

    public class ItemShortage
    {
        public string ItemID { get; set; }
        public string Location { get; set; }
        public DateTime? NeedDate { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public int? ScheduledQuantity { get; set; }
        public string ScheduledStatus { get; set; }
        public DateTime? AvailableDate { get; set; }
        public string DemandType { get; set; }
        public string ExternalOrderID { get; set; }
        public int? Priority { get; set; }

        public override string ToString()
        {
            return string.Empty;
            //return $"Resource: {Resource}, StartTime: {StartTimeNumber}, FinishTime: {FinishTimeNumber}, Id: {Id}";
        }
    }
}
