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
    using Orc.FilterBuilder;
    using Orc.Theming;
    using Catel.Configuration;
    using Gum;
    using Catel.IoC;
    using System.Threading.Tasks;

    public class ItemRoutingsDataGridViewModel : IntegratedDataGridViewModelBase<FactoryPlanningProject, ItemRouting>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public ItemRoutingsDataGridViewModel(DataGridContext context, IProjectManager projectManager, IProjectStateService projectStateService,
            IFilterService filterService, IDispatcherService dispatcherService, IProjectDataGridService projectDataGridService, ITabService tabService,
            IAccentColorService accentColorService, IConfigurationService configurationService, IScopeManager scopeManager, IServiceLocator serviceLocator)
            : base(context, projectManager, projectStateService, filterService, dispatcherService, projectDataGridService, tabService, accentColorService,
                  configurationService, scopeManager, serviceLocator)
        {
        }

        protected override async Task<IList<ItemRouting>> GetProjectRecordsAsync(FactoryPlanningProject project)
        {
            if (project is null)
            {
                return new List<ItemRouting>();
            }

            return project.ItemRoutings;
        }

        protected override string GetTabScope()
        {
            return ScopeNames.ItemRoutes;
        }
    }
}
