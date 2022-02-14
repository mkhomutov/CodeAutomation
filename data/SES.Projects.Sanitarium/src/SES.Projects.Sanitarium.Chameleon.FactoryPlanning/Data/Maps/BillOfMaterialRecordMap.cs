namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    internal sealed class BillOfMaterialRecordMap : Orc.Csv.ClassMapBase<BillOfMaterialRecord>
    {
        public BillOfMaterialRecordMap()
        {
            // Map booleans using .AsBool()
            // Map date/times using .AsDateTime() or .AsNullableDateTime()

            Map(m => m.ChildItemCode).Name("#ITEM","ITEM");
            Map(m => m.ParentItemCode).Name("SUBORD");
            Map(m => m.Location).Name("LOC");
            Map(m => m.StartDate).Name("EFF");
            Map(m => m.EndDate).Name("DISC");
            Map(m => m.DrawQuantity).Name("DRAWQTY");
            Map(m => m.BillOfMaterialNumber).Name("BOMNUM");
        }
    }
}
