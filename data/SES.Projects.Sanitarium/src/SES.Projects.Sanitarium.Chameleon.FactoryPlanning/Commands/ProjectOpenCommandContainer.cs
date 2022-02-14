namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning
{
    using Catel.MVVM;
    using Catel.Services;
    using Orc.ProjectManagement;
    using Gum.Commands;

    public class ProjectOpenCommandContainer : ProjectOpenAsDirectoryCommandContainerBase
    {
        public ProjectOpenCommandContainer(ICommandManager commandManager, IProjectManager projectManager,
            ISelectDirectoryService selectDirectoryService, IDispatcherService dispatcherService)
            : base(commandManager, projectManager, selectDirectoryService, dispatcherService)
        {

        }
    }
}
