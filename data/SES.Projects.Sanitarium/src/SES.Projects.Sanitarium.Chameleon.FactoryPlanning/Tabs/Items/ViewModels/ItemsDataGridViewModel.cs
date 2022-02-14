namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ViewModels
{
    using System.Collections.Generic;
    using Catel.Logging;
    using Catel.Services;
    using Gum.Controls;
    using Gum.Controls.Services;
    using Gum.Controls.ViewModels;
    using Gum.Services;
    using Orc.ProjectManagement;
    using ProjectManagement;
    using Catel.Configuration;
    using Gum;
    using Catel.IoC;
    using Orc.Theming;
    using Orc.FilterBuilder;
    using System.Threading.Tasks;
    using Gum.Projects.Controls.Configuration;
    using Gum.Projects;
    using Gum.Controls.Adapters;

    public class ItemsDataGridViewModel : IntegratedDataGridViewModelBase<FactoryPlanningProject, Item>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly DataGridConfiguration<Item> _dataGridConfiguration;

        public ItemsDataGridViewModel(DataGridContext context, IProjectManager projectManager, IProjectStateService projectStateService,
            IFilterService filterService, IDispatcherService dispatcherService, IProjectDataGridService projectDataGridService, ITabService tabService,
            IAccentColorService accentColorService, IConfigurationService configurationService, IScopeManager scopeManager, IServiceLocator serviceLocator)
            : base(context, projectManager, projectStateService, filterService, dispatcherService, projectDataGridService, tabService, accentColorService,
                  configurationService, scopeManager, serviceLocator)
        {
            _dataGridConfiguration = CreateDataGridConfiguration();
        }

        protected override IPropertyProvider GetPropertyProvider()
        {
            return _dataGridConfiguration.PropertyProvider;
        }

        protected override ISettingsAdapter GetSettingsAdapter()
        {
            return _dataGridConfiguration.SettingsAdapter;
        }

        protected override async Task<IList<Item>> GetProjectRecordsAsync(FactoryPlanningProject project)
        {
            if (project is null)
            {
                return new List<Item>();
            }

            return project.Items;
        }

        protected override string GetTabScope()
        {
            return ScopeNames.Items;
        }

        protected override void InitializeDataGrid(IDataGridInteraction interaction)
        {
            interaction.ApplyDefaultDataGridConfiguration(_dataGridConfiguration);

            interaction.ResetSettingsEx(WMScope, StringScope);
        }

        private DataGridConfiguration<Item> CreateDataGridConfiguration()
        {
            var dataGridConfiguration = new DataGridConfiguration<Item>();

            dataGridConfiguration.AddColumn(x => x.ItemId).Width(100);
            dataGridConfiguration.AddColumn(x => x.Description).Width(240);
            dataGridConfiguration.AddColumn(x => x.UnitsPerPallet).Width(100);
            dataGridConfiguration.AddColumn(x => x.UnitPrice).Width(100);
            dataGridConfiguration.AddColumn(x => x.DefaultUnitOfMeasure).DisplayName("UoM").Width(55);
            dataGridConfiguration.AddColumn(x => x.PackSize).Width(75).IsColored(true);
            dataGridConfiguration.AddColumn(x => x.Flavour).Width(75).IsColored(true);
            dataGridConfiguration.AddColumn(x => x.Division).Width(100).IsColored(true).IsGrouped(true, 1);
            dataGridConfiguration.AddColumn(x => x.Type).Width(100).IsColored(true).IsGrouped(true, 2);
            dataGridConfiguration.AddColumn(x => x.ClassificationLevel1).DisplayName("Level1").Width(160).IsColored(true);
            dataGridConfiguration.AddColumn(x => x.ClassificationLevel2).DisplayName("Level2").Width(160).IsColored(true);
            dataGridConfiguration.AddColumn(x => x.ClassificationLevel3).DisplayName("Level3").Width(160).IsColored(true);
            dataGridConfiguration.AddColumn(x => x.SendToBy).Width(100);
            dataGridConfiguration.AddColumn(x => x.ProductionFamily).Width(130);
            dataGridConfiguration.AddColumn(x => x.QaHoldDays).Width(60);

            return dataGridConfiguration;
        }
    }
}
