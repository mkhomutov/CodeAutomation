namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using Catel.Windows.Input;
    using Gum.Controls;
    using Orc.Theming;
    using System.Globalization;
    using System.Windows.Media;

    public static partial class SanitariumEnvironment
    {
        public static readonly CultureInfo Culture = new CultureInfo("en-AU");
    }

    public static class SanitariumSettings
    {
        // public const string MySetting = "Sanitarium.FactoryPlanning.MySetting";
        // public static readonly SomeType MySettingDefaultValue = null;

        public static class Validations
        {

        }
    }

    public static class SanitariumCommands
    {
        public static class Tabs
        {
            public const string Tear = "Tabs.Tear";
            public static readonly InputGesture TearInputGesture = null;
        }

        public static class Edit
        {
            public const string Production = "Edit.Production";
            public static readonly InputGesture EditProductionInputGesture = null;

            public const string AddOrder = "Edit.AddOrder";
            public static readonly InputGesture AddOrderInputGesture = null;

            public const string Restore = "Edit.Restore";
            public static readonly InputGesture RestoreInputGesture = null;
        }

        public static class Views
        {
            public const string ShowInventory = "Views.ShowInventory";
            public static readonly InputGesture ShowInventoryInputGesture = null;

            public const string ShowResources = "Views.ShowResources";
            public static readonly InputGesture ShowResourcesInputGesture = null;
        }
    }

    public static class FileNameFilters
    {
        public const string CsvFiles = "Csv files (*.csv)|*.csv";
    }

    public static class DirectoryNames
    {
        public const string SharedData = "SharedData";
    }

    public static class FileNames
    {
		public const string BillOfMaterials = "BOM.csv";
		public const string ItemShortages = "DemandShorts.csv";
		public const string Items = "ITEM.csv";
		public const string StockTargets = "StockTargets.csv";
		public const string TransitItems = "intransits.csv";
		public const string Operations = "PlanOrder.csv";
		public const string ItemRoutes = "ProductionRouting.csv";
		public const string ResourceUtilisation = "ResProjStatic.csv";
		public const string ItemInventoryOutcomes = "SKUProjStatic.csv";
        public const string EditedRecords = "EditedRecords.csv";
        public const string OverriddenValues = "OverriddenValues.csv";
        public const string ScenarioMappings = "ScenarioMappings.csv";
    }

    public static class ScopeNames
    {
        public const string Default = "default";
		public const string BillOfMaterials = "BillOfMaterials";
		public const string ItemShortages = "ItemShortages";
		public const string Items = "Items";
		public const string LineView = "LineView";
		public const string ItemDemands = "ItemDemands";
		public const string ItemRoutes = "ItemRoutes";
		public const string Operations = "Operations";
		public const string ResourceUtilisation = "ResourceUtilisation";
		public const string ItemInventoryMovements = "ItemInventoryOutcomes";
		public const string ItemView = "ItemView";
		public const string ProcessView = "ProcessView";
    }

    public static class SupplyTypes
    {
        public const string Planned = "Planned";
        public const string Firmed = "Firmed";
        public const string Combined = "Combined";
        public const string Mixed = "Mixed";
    }

    public static class ItemTypes
    {
        public const string FinishedGoods = "Finished Goods";
        public const string StoredInt = "Stored_int";
        public const string Intermediate = "Intermediate";
    }

    public static class LedgerTypes
    {
        public const string Demand = "Demand";
        public const string Supply = "Supply";
        public const string Total = "Total";
        public const string Inventory = "Inventory";
        public const string Cover = "Cover";
        public const string Policy = "Policy";
    }

    public static class LedgerNames
    {
        public const string TotalReceipts = "Total Receipts";
        public const string CustomerOrders = "Customer Orders";
        public const string ForecastOrders = "Forecast Orders";
        public const string ProductionConsumption = "Production Consumption";
        public const string ToShip = "To Ship";
        public const string TotalDemand = "Total Demand";
        public const string BeginningInventory = "Beginning Inventory";
        public const string EndingInventory = "Ending Inventory";
        public const string NetChange = "Net Change";
        public const string InTransit = "In Transit";
        public const string DaysCover = "Days Cover";
        public const string SafetyStock = "Safety Stock";
        public const string MaxQuantityCover = "Max Quantity Cover";
        public const string AboveAim = "Above Aim";
        public const string BelowAim = "Below Aim";
        public const string QaRelease = "QA Release";
    }

    public static class InventoryColors
    {
        public static Color Magenta = Colors.Magenta.MakeLighter2(75);
        public static Color Blue = Colors.Aqua.MakeLighter2(75);
        public static Color Yellow = Colors.Yellow.MakeLighter2(75);
        public static Color Red = Colors.Red.MakeLighter2(90);
        public static Color White = Colors.White.MakeLighter2(75);
        public static Color Salmon = Colors.Red.MakeLighter2(75);

        public static Color MakeLighter2(this Color color, byte a)
        {
            color = color.RemoveAlpha();
            return Color.FromArgb(a, color.R, color.G, color.B);
        }
    }

    

    public static class QuantityTypes
    {
        public const string Cases = "Cases";
        public const string Batches = "Batches";
        public const string Duration = "Duration";
    }

    public static class ConfigurationKeys
    {
        public const string ActiveScenario = "ActiveScenario";
    }

    public static class ScenarioNames
    {
        public const string AnyWorkspaceName = "< ANY >";
    }
}
