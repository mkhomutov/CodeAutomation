namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Logging;
    using Catel.Messaging;
    using Catel.MVVM;
    using CsvHelper.Configuration;
    using Orc.Csv;
    using Orc.FileSystem;
    using Orc.ProjectManagement;
    using Models;
    using ProjectManagement;

    public class ExportImportRibbonViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IProjectManager _projectManager;
        private readonly IDirectoryService _directoryService;
        private readonly ICsvWriterService _csvWriterService;
        private readonly ICsvReaderService _csvReaderService;

        public ExportImportRibbonViewModel(IProjectManager projectManager, IDirectoryService directoryService, ICsvWriterService csvWriterService,
            ICsvReaderService csvReaderService)
        {
            Argument.IsNotNull(() => projectManager);
            Argument.IsNotNull(() => directoryService);
            Argument.IsNotNull(() => csvWriterService);
            Argument.IsNotNull(() => csvReaderService);

            ExportAsync = new TaskCommand(OnExportAsync);
            _projectManager = projectManager;
            _directoryService = directoryService;
            _csvWriterService = csvWriterService;
            _csvReaderService = csvReaderService;
        }

        public TaskCommand ExportAsync { get; }

        private async Task OnExportAsync()
        {
            var project = _projectManager.GetActiveProject<FactoryPlanningProject>();

            var fileName = Path.Combine(project.Location, "Export", "OrdersOut.csv");

            await _csvReaderService.LoadAndApplySharedAsync(project);
        }

        protected async Task SaveRecordsAsync<TRecord, TRecordMap>(IEnumerable<TRecord> records, string fileName)
            where TRecordMap : ClassMap, new()
        {
            try
            {
                var directory = Path.GetDirectoryName(fileName);
                _directoryService.Create(directory);

                var csvContext = new CsvContext<TRecord, TRecordMap>
                {
                    Culture = SanitariumEnvironment.Culture,
                };

                csvContext.Configuration = _csvWriterService.CreateDefaultConfiguration(csvContext);

                // Enable this configuration if values contain quotes (e.g. 3" bottle)
                //csvContext.Configuration.IgnoreQuotes = true;

                await _csvWriterService.WriteRecordsAsync(records, fileName, csvContext);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Failed to save records data to '{fileName}'");
                throw;
            }
        }
    }
}
