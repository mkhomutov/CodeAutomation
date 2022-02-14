using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Models;

namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    public class Item
    {
        public Item()
        {
            InventoryMovements = new List<ItemInventoryMovement>();

            BeginningInventoryColorByWeek = new Dictionary<string, Color>();
            EndingInventoryColorByWeek = new Dictionary<string, Color>();
            ItemRoutes = new List<ItemRouting>();
            FirstStepItemRoutesByResource = new Dictionary<string, List<ItemRouting>>();
            WeeklyConsumptionByResource = new Dictionary<string, Dictionary<DateTime, double>>();
            SortedWeeks  = new List<DateTime>();
            TransitItems = new List<TransitItem>();
            BomRecords = new HashSet<BillOfMaterialRecord>();
            ParentItems = new List<Item>();
            ChildItems = new List<Item>();
            StockTargets = new List<StockTarget>();
            Overrides = new Dictionary<string, Dictionary<DateTime, double>>();

            Overrides.Add(LedgerNames.ToShip, new Dictionary<DateTime, double>());
            Overrides.Add(LedgerNames.QaRelease, new Dictionary<DateTime, double>());
        }

        public void Reset()
        {
            IsFinalStep = false;

            InventoryMovements.Clear();

            BeginningInventoryColorByWeek.Clear();
            EndingInventoryColorByWeek.Clear();
            ItemRoutes.Clear();
            FirstStepItemRoutesByResource.Clear();
            WeeklyConsumptionByResource.Clear();
            SortedWeeks.Clear();
            TransitItems.Clear();
            BomRecords.Clear();
            ParentItems.Clear();
            ChildItems.Clear();
            StockTargets.Clear();

            Overrides[LedgerNames.ToShip].Clear();
            Overrides[LedgerNames.QaRelease].Clear();
        }

        public string ItemId { get; set; }
        public string Description { get; set; }
        public int? UnitsPerPallet { get; set; }
        public double? UnitPrice { get; set; }
        public string DefaultUnitOfMeasure { get; set; }
        public string PackSize { get; set; }
        public string Flavour { get; set; }
        public string Division { get; set; }
        public string Type { get; set; }
        public string ClassificationLevel1 { get; set; }
        public string ClassificationLevel2 { get; set; }
        public string ClassificationLevel3 { get; set; }
        public int? SendToBy { get; set; }
        public string ProductionFamily { get; set; }
        public int QaHoldDays { get; set; }
        public bool IsFinalStep { get; set; }

        public List<Item> ParentItems { get; }
        public List<Item> ChildItems { get; }
        public List<StockTarget> StockTargets { get; }
        public List<TransitItem> TransitItems { get; }
        public List<ItemInventoryMovement> InventoryMovements { get; }
        public Dictionary<string, List<ItemRouting>> FirstStepItemRoutesByResource { get; } // Need to account for the different date ranges
        public List<ItemRouting> ItemRoutes { get; }
        public HashSet<BillOfMaterialRecord> BomRecords { get; }
        public Dictionary<string, Color> BeginningInventoryColorByWeek { get; }
        public Dictionary<string, Color> EndingInventoryColorByWeek { get; }
        public Dictionary<string, Dictionary<DateTime, double>> WeeklyConsumptionByResource { get; }
        public Dictionary<string, Dictionary<DateTime, double>> Overrides { get; }

        private List<DateTime> SortedWeeks { get; set; }

        public ItemRouting GetRouting(string resource, DateTime week)
        {
            var routings = FirstStepItemRoutesByResource[resource].Where(x => week > x.EffectiveDate && week < x.DiscontinueDate).ToList();
            return routings.SingleOrDefault(x => x.Resource.Equals(resource));
        }

        public List<ItemRouting> GetRoutings(DateTime week)
        {
            var routings = FirstStepItemRoutesByResource.Values.SelectMany(x => x).Where(x => week > x.EffectiveDate && week < x.DiscontinueDate).OrderBy(x => x.Resource).ToList();
            return routings;
        }

        public void InitialiseItemRoutesByResource(IEnumerable<ItemRouting> itemRoutes)
        {
            ItemRoutes.AddRange(itemRoutes);

            var itemRoutesByResource = itemRoutes.Where(x => x.StepNumber == 1).GroupBy(x => x.Resource).ToDictionary(x => x.Key, x => x.OrderBy(y => y.EffectiveDate).ToList());

            foreach (var itemRoutesInResource in itemRoutesByResource)
            {
                FirstStepItemRoutesByResource.Add(itemRoutesInResource.Key, itemRoutesInResource.Value);
            }
        }

        public override string ToString()
        {
            return $"{ItemId} - {Description} - {PackSize}";
        }
    }
}
