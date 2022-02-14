namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Catel.Configuration;
    using Catel.IoC;
    using Catel.Logging;
    using Catel.Services;
    using Gum;
    using Gum.Controls;
    using Gum.Controls.Services;
    using Gum.Controls.ViewModels;
    using Gum.Controls.WorkspaceManagement;
    using Gum.Projects.Controls.Configuration;
    using Gum.Services;
    using Orc.FilterBuilder;
    using Orc.ProjectManagement;
    using Orc.Theming;
    using ProjectManagement;

    public class BillOfMaterialRecordsDataGridViewModel : IntegratedDataGridViewModelBase<FactoryPlanningProject, BillOfMaterialRecord>
    {
        #region Fields
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly IDispatcherService _dispatcherService;
        private readonly IProjectManager _projectManager;
        #endregion

        #region Constructors
        public BillOfMaterialRecordsDataGridViewModel(DataGridContext context, IProjectManager projectManager, IProjectStateService projectStateService,
            IFilterService filterService, IDispatcherService dispatcherService, IProjectDataGridService projectDataGridService, ITabService tabService,
            IAccentColorService accentColorService, IConfigurationService configurationService, IScopeManager scopeManager, IServiceLocator serviceLocator)
            : base(context, projectManager, projectStateService, filterService, dispatcherService, projectDataGridService, tabService, accentColorService,
                configurationService, scopeManager, serviceLocator)
        {
            _projectManager = projectManager;
            _dispatcherService = dispatcherService;
        }
        #endregion

        #region Methods
        protected override Task InitializeAsync()
        {
            _projectManager.ProjectActivatedAsync += OnProjectActivatedAsync;

            return base.InitializeAsync();
        }

        protected override Task CloseAsync()
        {
            _projectManager.ProjectActivatedAsync -= OnProjectActivatedAsync;

            return base.CloseAsync();
        }

        private async Task OnProjectActivatedAsync(object sender, ProjectUpdatedEventArgs e)
        {
            if (e.NewProject is null)
            {
                return;
            }

            _dispatcherService.Invoke(ApplyConfiguration);
        }

        protected override void InitializeDataGrid(IDataGridInteraction interaction)
        {
            ApplyConfiguration();
        }

        private void ApplyConfiguration()
        {
            var interaction = Interaction;
            if (interaction is null)
            {
                return;
            }

            using (interaction.SuspendUpdate(false, true))
            {
                var dataGridConfiguration = CreateDataGridConfiguration();

                interaction.SetPropertyProvider(dataGridConfiguration.PropertyProvider);
                interaction.SetSettingAdapter(dataGridConfiguration.SettingsAdapter);
            }

            interaction.ResetSettingsEx(WMScope, StringScope);
        }

        protected override DataGridWorkspaceProvider GetWorkspaceProvider()
        {
            return null;
        }

        private DataGridConfiguration<BillOfMaterialRecord> CreateDataGridConfiguration()
        {
            var dataGridConfiguration = new DataGridConfiguration<BillOfMaterialRecord>();

            dataGridConfiguration.IsCellMergingEnabled(true);
            dataGridConfiguration.FontSize(10);

            dataGridConfiguration.AddColumn(x => x.Location).IsVisible(true).IsColored(true);
            dataGridConfiguration.AddColumn(x => x.BillOfMaterialNumber).IsVisible(true);
            dataGridConfiguration.AddColumn(x => x.ChildItemCode).IsVisible(true).DisplayName("Parent Item");
            dataGridConfiguration.AddColumn(x => x.DrawQuantity).IsVisible(true);
            dataGridConfiguration.AddColumn(x => x.EndDate).IsVisible(true);
            dataGridConfiguration.AddColumn(x => x.ParentItemCode).IsVisible(true).DisplayName("Child Item");
            dataGridConfiguration.AddColumn(x => x.StartDate).IsVisible(true);

            return dataGridConfiguration;
        }

        protected override async Task<IList<BillOfMaterialRecord>> GetProjectRecordsAsync(FactoryPlanningProject project)
        {
            if (project is null)
            {
                return new List<BillOfMaterialRecord>();
            }

            return project.BillOfMaterialRecords;
        }

        protected override string GetTabScope()
        {
            return ScopeNames.BillOfMaterials;
        }
        #endregion
    }
}
