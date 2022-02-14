namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    internal sealed class ItemShortageMap : Orc.Csv.ClassMapBase<ItemShortage>
    {
        public ItemShortageMap()
        {
            // Map booleans using .AsBool()
            // Map date/times using .AsDateTime() or .AsNullableDateTime()

            Map(m => m.ItemID).Name("#ITEM", "ITEM");
            Map(m => m.Location).Name("LOC");
            Map(m => m.NeedDate).Name("NEEDDATE");
            Map(m => m.Quantity).Name("QTY");
            Map(m => m.ScheduledDate).Name("SCHEDDATE");
            Map(m => m.ScheduledQuantity).Name("SCHEDQTY");
            Map(m => m.ScheduledStatus).Name("SCHEDSTATUS");
            Map(m => m.AvailableDate).Name("AVAILDATE");
            Map(m => m.DemandType).Name("DMDTYPE");
            Map(m => m.ExternalOrderID).Name("EXTORDERID");
            Map(m => m.Priority).Name("PRIORITY");
        }
    }
}
