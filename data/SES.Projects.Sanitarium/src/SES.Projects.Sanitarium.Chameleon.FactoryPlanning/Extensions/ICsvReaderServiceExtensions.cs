
namespace SES.Projects.Sanitarium.Chameleon
{
    using System.IO;
    using System.Runtime.CompilerServices;
    using Catel.Logging;
    using CsvHelper.Configuration;
    using Orc.Csv;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Catel.IoC;
    using Catel.Messaging;
    using FactoryPlanning;
    using FactoryPlanning.Models;
    using FactoryPlanning.ProjectManagement;

    public static class ICsvReaderServiceExtensions
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();
        private static readonly IMessageMediator MessageMediator;

        static ICsvReaderServiceExtensions()
        {
            MessageMediator = ServiceLocator.Default.ResolveType<IMessageMediator>();
        }

        public static async Task<List<TRecord>> LoadRecordsAsync<TRecord, TRecordMap>(this ICsvReaderService csvReaderService, string fileName)
            where TRecordMap : ClassMap, new()
        {
            var result = new List<TRecord>();

            try
            {
                if (!File.Exists(fileName))
                {
                    return result;
                }

                var csvContext = new CsvContext<TRecord, TRecordMap>
                {
                    Culture = SanitariumEnvironment.Culture,
                    ThrowOnError = false,
                };

                csvContext.Configuration = csvReaderService.CreateDefaultConfiguration(csvContext);
                //csvContext.Configuration.ShouldSkipRecord = (x) => x.Record.All(y => string.IsNullOrWhiteSpace(y)); // Very slow

                csvContext.Configuration.IgnoreBlankLines = true;

                // Enable this configuration if values contain quotes (e.g. 3" bottle)
                //csvContext.Configuration.IgnoreQuotes = true;

                var records = await csvReaderService.ReadRecordsAsync<TRecord>(fileName, csvContext);
                if (records is not null)
                {
                    result.AddRange(records);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to load records data from '{fileName}'");
                throw;
            }

            return result;
        }

        public static async Task LoadAndApplySharedAsync(this ICsvReaderService csvReaderService, FactoryPlanningProject project)
        {
            await csvReaderService.LoadProjectFilesAsync(project);

            project.Initialise();

            MessageMediator.SendMessage(new UiUpdateRequiredMsg(project));
        }


        public static async Task LoadProjectFilesAsync(this ICsvReaderService csvReaderService, FactoryPlanningProject project)
        {
            var location = project.Location;

            project.BillOfMaterialRecords.Clear();
            var bomRecords = await csvReaderService.LoadBillOfMaterialsAsync(Path.Combine(location, FileNames.BillOfMaterials));
            project.BillOfMaterialRecords.AddRange(bomRecords);

            project.ItemShortages.Clear();
            var itemShortages = await csvReaderService.LoadItemShortagesAsync(Path.Combine(location, FileNames.ItemShortages));
            project.ItemShortages.AddRange(itemShortages);

            project.Items.Clear();
            var items = await csvReaderService.LoadItemsAsync(Path.Combine(location, FileNames.Items));
            project.Items.AddRange(items);

            project.StockTargets.Clear();
            var stockTargets = await csvReaderService.LoadStockTargetsAsync(Path.Combine(location, FileNames.StockTargets));
            project.StockTargets.AddRange(stockTargets);

            project.TransitItems.Clear();
            var transitItems = await csvReaderService.LoadTransitItemsAsync(Path.Combine(location, FileNames.TransitItems));
            project.TransitItems.AddRange(transitItems);

            project.ItemRoutings.Clear();
            var itemRoutes = await csvReaderService.LoadItemRoutesAsync(Path.Combine(location, FileNames.ItemRoutes));
            project.ItemRoutings.AddRange(itemRoutes);

            project.ResourceUtilisations.Clear();
            var capacityCalendars = await csvReaderService.LoadCapacityCalendarsAsync(Path.Combine(location, FileNames.ResourceUtilisation));
            project.ResourceUtilisations.AddRange(capacityCalendars);

            project.ItemInventoryMovements.Clear();
            var itemInventoryOutcomes = await csvReaderService.LoadItemInventoryOutcomesAsync(Path.Combine(location, FileNames.ItemInventoryOutcomes));
            project.ItemInventoryMovements.AddRange(itemInventoryOutcomes);
        }

        private static Task<List<BillOfMaterialRecord>> LoadBillOfMaterialsAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<BillOfMaterialRecord, BillOfMaterialRecordMap>(fileName);
        }

        private static Task<List<ItemShortage>> LoadItemShortagesAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<ItemShortage, ItemShortageMap>(fileName);
        }

        private static Task<List<Item>> LoadItemsAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<Item, ItemMap>(fileName);
        }

        private static Task<List<StockTarget>> LoadStockTargetsAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<StockTarget, StockTargetMap>(fileName);
        }

        private static Task<List<TransitItem>> LoadTransitItemsAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<TransitItem, TransitItemMap>(fileName);
        }

        private static Task<List<ItemRouting>> LoadItemRoutesAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<ItemRouting, ItemRoutingMap>(fileName);
        }

        private static Task<List<ResourceUtilisation>> LoadCapacityCalendarsAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<ResourceUtilisation, ResourceUtilisationMap>(fileName);
        }

        private static Task<List<ItemInventoryMovement>> LoadItemInventoryOutcomesAsync(this ICsvReaderService csvReaderService, string fileName)
        {
            return csvReaderService.LoadRecordsAsync<ItemInventoryMovement, ItemInventoryMovementMap>(fileName);
        }
    }
}
