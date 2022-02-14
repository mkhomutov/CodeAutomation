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

    public class ItemShortagesDataGridViewModel : IntegratedDataGridViewModelBase<FactoryPlanningProject, ItemShortage>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public ItemShortagesDataGridViewModel(DataGridContext context, IProjectManager projectManager, IProjectStateService projectStateService,
            IFilterService filterService, IDispatcherService dispatcherService, IProjectDataGridService projectDataGridService, ITabService tabService,
            IAccentColorService accentColorService, IConfigurationService configurationService, IScopeManager scopeManager, IServiceLocator serviceLocator)
            : base(context, projectManager, projectStateService, filterService, dispatcherService, projectDataGridService, tabService, accentColorService,
                  configurationService, scopeManager, serviceLocator)
        {
        }

        protected override async Task<IList<ItemShortage>> GetProjectRecordsAsync(FactoryPlanningProject project)
        {
            if (project is null)
            {
                return new List<ItemShortage>();
            }

            return project.ItemShortages;
        }

        protected override string GetTabScope()
        {
            return ScopeNames.ItemShortages;
        }
    }
}
