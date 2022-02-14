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

    public class ResourceUtilisationsDataGridViewModel : IntegratedDataGridViewModelBase<FactoryPlanningProject, ResourceUtilisation>
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        public ResourceUtilisationsDataGridViewModel(DataGridContext context, IProjectManager projectManager, IProjectStateService projectStateService,
            IFilterService filterService, IDispatcherService dispatcherService, IProjectDataGridService projectDataGridService, ITabService tabService,
            IAccentColorService accentColorService, IConfigurationService configurationService, IScopeManager scopeManager, IServiceLocator serviceLocator)
            : base(context, projectManager, projectStateService, filterService, dispatcherService, projectDataGridService, tabService, accentColorService,
                  configurationService, scopeManager, serviceLocator)
        {
        }

        protected override async Task<IList<ResourceUtilisation>> GetProjectRecordsAsync(FactoryPlanningProject project)
        {
            return project is null ? new List<ResourceUtilisation>() : project.ResourceUtilisations;
        }

        protected override string GetTabScope()
        {
            return ScopeNames.ResourceUtilisation;
        }
    }
}
