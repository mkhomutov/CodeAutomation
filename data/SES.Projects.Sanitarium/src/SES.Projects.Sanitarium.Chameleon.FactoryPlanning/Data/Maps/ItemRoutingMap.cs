namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    internal sealed class ItemRoutingMap : Orc.Csv.ClassMapBase<ItemRouting>
    {
        public ItemRoutingMap()
        {
            // Map booleans using .AsBool()
            // Map date/times using .AsDateTime() or .AsNullableDateTime()

            Map(m => m.ItemID).Name("#ITEM", "ITEM");
            Map(m => m.Location).Name("LOC");
            Map(m => m.ProductionMethod).Name("PRODUCTIONMETHOD");
            Map(m => m.Resource).Name("RES");
            Map(m => m.Priority).Name("PRIORITY");
            Map(m => m.StepNumber).Name("STEPNUM");
            Map(m => m.EffectiveDate).Name("EFF");
            Map(m => m.DiscontinueDate).Name("DISC");
            Map(m => m.MinimumQuantity).Name("MINQTY");
            Map(m => m.IncrementalQuantity).Name("INCQTY");
            Map(m => m.BatchSizeRaw).Name("BatchSize"); // Yes camel case...
            Map(m => m.BillOfMaterialNumber).Name("BOMNUM");
            Map(m => m.NoNewSupplyDate).Name("NONEWSUPPLYDATE");
            //Map(m => m.Prodcost).Name("PRODCOST");
            Map(m => m.NextStepTiming).Name("NEXTSTEPTIMING");
            Map(m => m.ProductionRate).Name("PRODRATE");
            Map(m => m.ProductionDuration).Name("PRODDUR");
            Map(m => m.ProductionRateCalendar).Name("PRODRATECAL");
            Map(m => m.ProductionVersion).Name("PRODVERSION");
        }
    }
}
