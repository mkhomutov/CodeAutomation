namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using Catel.MVVM;
    using Catel.Services;
    using Gum;
    using Orc.ProjectManagement;

    public class ProjectSaveAsCommandContainer : ProjectSaveAsDirectoryCommandContainerBase
    {
        public ProjectSaveAsCommandContainer(ICommandManager commandManager, IProjectManager projectManager,
            ISelectDirectoryService selectDirectoryService, IDispatcherService dispatcherService)
            : base(commandManager, projectManager, selectDirectoryService, dispatcherService)
        {
        }
    }
}
