namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Catel;
    using Catel.Configuration;
    using Catel.Logging;
    using Catel.Messaging;
    using Catel.Services;
    using CsvHelper.Configuration;
    using Gum.ProjectManagement;
    using MethodTimer;
    using Orc.Controls.Converters;
    using Orc.Csv;
    using Orc.FileSystem;
    using Orc.ProjectManagement;
    using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Models;

    public class ProjectWriter : Gum.ProjectManagement.ProjectWriterBase<FactoryPlanningProject>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();


        private readonly ICsvWriterService _csvWriterService;
        private readonly IPleaseWaitService _pleaseWaitService;
        private readonly IDirectoryService _directoryService;
        private readonly ICsvReaderService _csvReaderService;
        private readonly IProjectManager _projectManager;

        public ProjectWriter(ICsvWriterService csvWriterService, IPleaseWaitService pleaseWaitService, IDirectoryService directoryService,
            IFileService fileService, IProjectSettingsSerializationService projectSettingsSerializationService,
            IConfigurationService configurationService, ICsvReaderService csvReaderService,
            IProjectManager projectManager)
            : base(directoryService, fileService, projectSettingsSerializationService, configurationService)
        {
            Argument.IsNotNull(() => csvWriterService);
            Argument.IsNotNull(() => pleaseWaitService);
            Argument.IsNotNull(() => csvReaderService);
            Argument.IsNotNull(() => projectManager);

            _csvWriterService = csvWriterService;
            _pleaseWaitService = pleaseWaitService;
            _directoryService = directoryService;
            _csvReaderService = csvReaderService;
            _projectManager = projectManager;
        }

        [Time]
        protected override async Task<bool> WriteToLocationAsync(FactoryPlanningProject project, string location, ProjectSettings projectSettings)
        {
            await _csvReaderService.LoadAndApplySharedAsync(project);

            return true;
        }

        private Task SavePartRelationshipsAsync(IEnumerable<BillOfMaterialRecord> records, string fileName)
        {
            return _csvWriterService.SaveRecordsAsync<BillOfMaterialRecord, BillOfMaterialRecordMap>(records, fileName);
        }

        private Task SaveItemShortagesAsync(IEnumerable<ItemShortage> records, string fileName)
        {
            return _csvWriterService.SaveRecordsAsync<ItemShortage, ItemShortageMap>(records, fileName);
        }

        private Task SaveItemsAsync(IEnumerable<Item> records, string fileName)
        {
            return _csvWriterService.SaveRecordsAsync<Item, ItemMap>(records, fileName);
        }

        private Task SaveItemRoutesAsync(IEnumerable<ItemRouting> records, string fileName)
        {
            return _csvWriterService.SaveRecordsAsync<ItemRouting, ItemRoutingMap>(records, fileName);
        }

        private Task SaveCapacityCalendarsAsync(IEnumerable<ResourceUtilisation> records, string fileName)
        {
            return _csvWriterService.SaveRecordsAsync<ResourceUtilisation, ResourceUtilisationMap>(records, fileName);
        }

        private Task SaveItemInventoryOutcomesAsync(IEnumerable<ItemInventoryMovement> records, string fileName)
        {
            return _csvWriterService.SaveRecordsAsync<ItemInventoryMovement, ItemInventoryMovementMap>(records, fileName);
        }
    }
}
