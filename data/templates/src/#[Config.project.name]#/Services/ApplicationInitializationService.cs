using System.Threading.Tasks;
using Catel;
using Catel.IoC;
using Catel.MVVM;
using Gum;
using Gum.Controls;
using Gum.Fonts;
using Gum.ProjectManagement;
using Gum.Services;
using Orc.Metadata;
using Orc.ProjectManagement;
using #[$Namespace(Config.project.name)]#.Data.ProjectManagement;
%VIEWMODELUSINGS%
using WildGums = Gum.Fonts.WildGums;
using Orchestra.Services;

namespace #[$Namespace(Config.project.name)]#.Services;

public class ApplicationInitializationService : Gum.Services.ApplicationInitializationServiceBase
{
    public override bool ShowSplashScreen => true;

    public override bool ShowShell => true;

    public ApplicationInitializationService(ITypeFactory typeFactory, IServiceLocator serviceLocator, ICloseApplicationService closeApplicationService)
        : base(typeFactory, serviceLocator, closeApplicationService)
    {
    }

    protected override void RegisterTypes(IServiceLocator serviceLocator)
    {
        base.RegisterTypes(serviceLocator);

        serviceLocator.RegisterType<IFirstRunService, FirstRunService>();

        serviceLocator.RegisterType<IProjectManager, ProjectManager>();
        serviceLocator.RegisterType<IProjectReader, ProjectReader>();
    }

    protected override void RegisterWatchers(IServiceLocator serviceLocator)
    {
        base.RegisterWatchers(serviceLocator);

        serviceLocator.RegisterTypeAndInstantiate<WorkspaceManagementProjectWatcher>();

        var localUpdatesWatcher = serviceLocator.RegisterTypeAndInstantiate<LocalApplicationUpdateSourcesWatcher>();
        localUpdatesWatcher.StartWatching();
    }

    public override async Task InitializeAfterShowingShellAsync()
    {
        await base.InitializeAfterShowingShellAsync();

        InitializeTabs();

        ShellActivatedActionQueue.EnqueueAction(async () =>
#pragma warning restore AvoidAsyncVoid
        {
            var applicationInstanceService = ServiceLocator.ResolveType<IApplicationInstanceService>();
            await LoadProjectAsync();

            applicationInstanceService?.Unlock();
        });
    }

    protected override void InitializeCommands(ICommandManager commandManager)
    {
        base.InitializeCommands(commandManager);

        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Open));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Save));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.SaveAs));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Refresh));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Close));
    }

    private void InitializeTabs()
    {
        var tabService = ServiceLocator.ResolveType<ITabService>();

        %TABITEMS%

        %ADDTABITEMS%

        tabService.Activate(%FIRSTTAB%);
    }

    protected override Task LoadProjectAsync()
    {
        return base.LoadProjectAsync();
    }
}
