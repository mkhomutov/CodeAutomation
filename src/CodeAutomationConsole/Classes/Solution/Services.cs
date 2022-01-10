namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Services
    {
        public Services(string ns, string projectPath, List<CsvListMember> csv)
        {
            ProjectPath = projectPath;

            var tabItems = csv.Select(x => $"var tabItem{x.ClassName} = tabService.CreateAndAddDataGridTab<{x.ClassName}DataGridViewModel>(\"{x.ClassName}\", ScopeNames.{x.ClassName}, false);").
                ToArray().
                JoinWithTabs(2);

            var usings = csv.Select(x => $"using {Global.Namespace}.UI.Tabs.{x.ClassName}.ViewModels;").ToArray().JoinWithTabs(0);

            var addTabItems = csv.Select(x => $"tabService.Add(tabItem{x.ClassName});").ToArray().JoinWithTabs(2);

            var firstTab = "tabItem" + csv.FirstOrDefault().ClassName;

            ApplicationInitializationServiceContent = $@"
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
using {ns}.Data.ProjectManagement;
{usings}
using WildGums = Gum.Fonts.WildGums;
using Orchestra.Services;

namespace {ns}.Services;

public class ApplicationInitializationService : Gum.Services.ApplicationInitializationServiceBase
{{
    public override bool ShowSplashScreen => true;

    public override bool ShowShell => true;

    public ApplicationInitializationService(ITypeFactory typeFactory, IServiceLocator serviceLocator, ICloseApplicationService closeApplicationService)
        : base(typeFactory, serviceLocator, closeApplicationService)
    {{
    }}
    
    protected override void RegisterTypes(IServiceLocator serviceLocator)
    {{
        base.RegisterTypes(serviceLocator);

        serviceLocator.RegisterType<IFirstRunService, FirstRunService>();

        serviceLocator.RegisterType<IProjectManager, ProjectManager>();
        serviceLocator.RegisterType<IProjectReader, ProjectReader>();
    }}

    protected override void RegisterWatchers(IServiceLocator serviceLocator)
    {{
        base.RegisterWatchers(serviceLocator);

        serviceLocator.RegisterTypeAndInstantiate<WorkspaceManagementProjectWatcher>();

        var localUpdatesWatcher = serviceLocator.RegisterTypeAndInstantiate<LocalApplicationUpdateSourcesWatcher>();
        localUpdatesWatcher.StartWatching();
    }}

    public override async Task InitializeAfterShowingShellAsync()
    {{
        await base.InitializeAfterShowingShellAsync();

        InitializeTabs();

        ShellActivatedActionQueue.EnqueueAction(async () =>
#pragma warning restore AvoidAsyncVoid
        {{
            var applicationInstanceService = ServiceLocator.ResolveType<IApplicationInstanceService>();
            await LoadProjectAsync();

            applicationInstanceService?.Unlock();
        }});
    }}

    protected override void InitializeCommands(ICommandManager commandManager)
    {{
        base.InitializeCommands(commandManager);

        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Open));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Save));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.SaveAs));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Refresh));
        commandManager.CreateCommandWithGesture(typeof(AppCommands.Project), nameof(AppCommands.Project.Close));
    }}

    private void InitializeTabs()
    {{
        var tabService = ServiceLocator.ResolveType<ITabService>();

        {tabItems}

        {addTabItems}

        tabService.Activate({firstTab});
    }}

    protected override Task LoadProjectAsync()
    {{
        return base.LoadProjectAsync();
    }}
}}
";

            RibbonServiceContent = $@"using System.Windows;
using Orchestra.Services;
using {ns}.UI.Views;

namespace {ns}.UI.Services;

public class RibbonService : IRibbonService
{{
    public FrameworkElement GetRibbon()
    {{
        return new RibbonView();
    }}

    public FrameworkElement GetMainView()
    {{
        return new MainView();
    }}

    public FrameworkElement GetStatusBar()
    {{
        return new StatusBarView();
    }}
}}
";
        }

        public string ProjectPath { get; set; }

        public string ApplicationInitializationServiceContent { get; set; }

        public string RibbonServiceContent { get; set; }

        public void Save()
        {
            var ApplicationInitializationServiceFile = Path.Combine(ProjectPath, "Services", "ApplicationInitializationService.cs");
            var RibbonServiceFile = Path.Combine(ProjectPath, "UI", "Services", "RibbonService.cs");

            ApplicationInitializationServiceContent.AddCopyright("ApplicationInitializationService.cs").SaveToFile(ApplicationInitializationServiceFile);
            RibbonServiceContent.SaveToFile(RibbonServiceFile);
        }

    }
}
