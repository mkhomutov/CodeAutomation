namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using Orc.Csv;
    using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Models;

    internal sealed class StockTargetMap : Orc.Csv.ClassMapBase<StockTarget>
    {
        public StockTargetMap()
        {
            Map(m => m.ItemId).Name("#ITEM", "ITEM");
            Map(m => m.Location).Name("LOC");
            Map(m => m.StartDate).Name("STARTDATE");
            Map(m => m.SafetyStock).Name("SAFETYSTOCK");
            Map(m => m.AboveTarget).Name("ABOVETARGET");
            Map(m => m.BelowTarget).Name("BELOWTARGET");
        }
    }
}
