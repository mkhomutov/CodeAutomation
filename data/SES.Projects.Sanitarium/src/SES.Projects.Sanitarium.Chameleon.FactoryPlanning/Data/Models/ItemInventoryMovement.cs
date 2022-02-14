namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using System;

    public class ItemInventoryMovement
    {
        public string ItemID { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartOfWeekDate { get; set; }
        public double? TotalDemand { get; set; }
        public double? CustomerOrderNeededUnits { get; set; }
        public double? ForecastOrderNeededUnits { get; set; }
        public double? SafetyStock { get; set; }
        public double? TotalShip { get; set; }
        public double? TotalSupply { get; set; }
        public double? OpeningInventory { get; set; }
        public double? ProjectedOnHand { get; set; }
        public double? MaximumQauntityCover { get; set; }

        public override string ToString()
        {
            return string.Empty;
            //return $"Resource: {Resource}, StartTime: {StartTimeNumber}, FinishTime: {FinishTimeNumber}, Id: {Id}";
        }
    }
}
