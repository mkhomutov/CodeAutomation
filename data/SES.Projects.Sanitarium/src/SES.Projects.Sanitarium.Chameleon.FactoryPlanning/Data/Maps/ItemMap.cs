namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using Orc.Csv;

    internal sealed class ItemMap : Orc.Csv.ClassMapBase<Item>
    {
        public ItemMap()
        {
            // Map booleans using .AsBool()
            // Map date/times using .AsDateTime() or .AsNullableDateTime()

            Map(m => m.ItemId).Name("#ITEM", "ITEM");
            Map(m => m.Description).Name("DESCR");
            Map(m => m.UnitsPerPallet).Name("UNITSPERPALLET");
            Map(m => m.UnitPrice).Name("UNITPRICE");
            Map(m => m.DefaultUnitOfMeasure).Name("DEFAULTUOM");
            Map(m => m.PackSize).Name("U_PACKSIZE");
            Map(m => m.Flavour).Name("U_FLAVOUR");
            Map(m => m.Division).Name("U_DIVISION");
            Map(m => m.Type).Name("U_TYPE");
            Map(m => m.ClassificationLevel1).Name("U_LEVEL_1");
            Map(m => m.ClassificationLevel2).Name("U_LEVEL_2");
            Map(m => m.ClassificationLevel3).Name("U_LEVEL_3");
            Map(m => m.SendToBy).Name("SENDTOBY");
            //Map(m => m.Perishablesw).Name("PERISHABLESW");
            Map(m => m.ProductionFamily).Name("U_PRODUCTIONFAMILY");
            Map(m => m.QaHoldDays).Name("U_QA_HOLD_TIME", "QAHOLDDAYS").Default(0);
        }
    }
}
