namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using System;
    using System.Collections.Generic;
    using Orc.ProjectManagement;
    using Models;
    using System.Linq;

    public class FactoryPlanningProject : ProjectBase
    {
        public FactoryPlanningProject(string location)
            : this(location, location)
        {
        }

        public FactoryPlanningProject(string location, string title)
            : base(location, title)
        {
            BillOfMaterialRecords = new List<BillOfMaterialRecord>();
            ItemShortages = new List<ItemShortage>();
            Items = new List<Item>();
            TransitItems = new List<TransitItem>();
            ItemRoutings = new List<ItemRouting>();
            ResourceUtilisations = new List<ResourceUtilisation>();
            ItemInventoryMovements = new List<ItemInventoryMovement>();
            StockTargets = new List<StockTarget>();
        }

        public List<BillOfMaterialRecord> BillOfMaterialRecords { get; }
        public List<ItemShortage> ItemShortages { get; }
        public List<Item> Items { get; }
        public List<StockTarget> StockTargets { get; }
        public List<TransitItem> TransitItems { get; }
        public List<ItemRouting> ItemRoutings { get; }
        public List<ResourceUtilisation> ResourceUtilisations { get; }
        public List<ItemInventoryMovement> ItemInventoryMovements { get; }
    }
}
