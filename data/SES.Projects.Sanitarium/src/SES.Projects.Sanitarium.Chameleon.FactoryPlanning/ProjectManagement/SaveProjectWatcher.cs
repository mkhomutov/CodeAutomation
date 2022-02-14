namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using System.Threading.Tasks;
    using Catel;
    using Catel.Configuration;
    using Gum.Services;
    using Orc.ProjectManagement;

    public class SaveProjectWatcher : ProjectWatcherBase
    {
        #region Fields
        private readonly ISaveProjectChangesService _saveProjectChangesService;
        #endregion

        #region Constructors
        public SaveProjectWatcher(IProjectManager projectManager, IConfigurationService configurationService,
            ISaveProjectChangesService saveProjectChangesService)
            : base(projectManager)
        {
            Argument.IsNotNull(() => configurationService);
            Argument.IsNotNull(() => saveProjectChangesService);

            _saveProjectChangesService = saveProjectChangesService;
        }
        #endregion

        #region Methods
        protected override async Task OnRefreshingAsync(ProjectCancelEventArgs e)
        {
            if (e.Cancel)
            {
                return;
            }

            var project = (FactoryPlanningProject)e.Project;

            ////TODO:Vladimir: track is dirty
            //project.MarkAsDirty();

            //if (!await _saveProjectChangesService.EnsureChangesSavedAsync(project, SaveChangesReason.Refreshing))
            //{
            //    e.Cancel = true;
            //    return;
            //}

            // Automatically save on refreshing. No need to ask users
            await ProjectManager.SaveAsync();

            await base.OnRefreshingAsync(e);
        }

        protected override async Task OnClosingAsync(ProjectCancelEventArgs e)
        {
            if (e.Cancel)
            {
                await base.OnClosingAsync(e);
                return;
            }

            var project = (FactoryPlanningProject)e.Project;

            // Automatically save on close. No need to ask users
            await ProjectManager.SaveAsync();

            ////TODO:Vladimir: track is dirty
            //project.MarkAsDirty();

            //if (!await _saveProjectChangesService.EnsureChangesSavedAsync(project, SaveChangesReason.Closing))
            //{
            //    e.Cancel = true;
            //    return;
            //}

            await base.OnClosingAsync(e);
        }
        #endregion
    }
}
