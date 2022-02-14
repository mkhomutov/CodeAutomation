namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    internal sealed class ItemInventoryMovementMap : Orc.Csv.ClassMapBase<ItemInventoryMovement>
    {
        public ItemInventoryMovementMap()
        {
            // Map booleans using .AsBool()
            // Map date/times using .AsDateTime() or .AsNullableDateTime()

            Map(m => m.ItemID).Name("#ITEM", "ITEM");
            Map(m => m.Description).Name("Desc", "DESCR");
            Map(m => m.StartDate).Name("STARTDATE");
            Map(m => m.TotalDemand).Name("TOTDMD");
            Map(m => m.CustomerOrderNeededUnits).Name("CONEEDUNIT");
            Map(m => m.ForecastOrderNeededUnits).Name("FONEEDUNIT");
            Map(m => m.SafetyStock).Name("SS");
            Map(m => m.TotalShip).Name("TOTSHIP");
            Map(m => m.TotalSupply).Name("TOTSUPPLY");
            Map(m => m.OpeningInventory).Name("INVENTORY");
            //Map(m => m.Planarriv).Name("PLANARRIV");
            //Map(m => m.Coschedunit).Name("COSCHEDUNIT");
            //Map(m => m.Foschedunit).Name("FOSCHEDUNIT");
            //Map(m => m.Metinddmd).Name("METINDDMD");
            Map(m => m.ProjectedOnHand).Name("PROJOH");
            Map(m => m.MaximumQauntityCover).Name("MaxCovDur", "MAXCOVDUR");
        }
    }
}
