namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.WorkspaceManagement.Providers
{
    using Catel;
    using Catel.IoC;
    using Catel.Services;
    using Orc.WorkspaceManagement;

    public abstract class WorkspaceProviderBase : Orc.WorkspaceManagement.WorkspaceProviderBase
    {
        protected WorkspaceProviderBase(IWorkspaceManager workspaceManager, IDispatcherService dispatcherService, IServiceLocator serviceLocator)
            : base(workspaceManager, serviceLocator)
        {
            Argument.IsNotNull(() => dispatcherService);

            DispatcherService = dispatcherService;
        }

        protected IDispatcherService DispatcherService { get; }
    }
}
