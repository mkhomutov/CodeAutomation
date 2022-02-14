namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Plugins
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using Catel;
    using Catel.IoC;
    using Catel.MVVM;
    using Catel.Reflection;
    using Fluent;
    using global::Chameleon;
    using global::Chameleon.Services;
    using Orc.ProjectManagement;
    using ProjectManagement;
    using Services;
    using Views;
    using Gum;
    using Gum.Attributes;
    using Gum.Controls;
    using Gum.Controls.Services;
    using Gum.Projects.Services;
    using Gum.Services;
    using ViewModels;
    using Application = System.Windows.Application;
    using TabbedDataGridScopeService = Services.TabbedDataGridScopeService;

    [PluginName("FactoryPlanning")]
    [ExceptionFiles("")]
    public class FactoryPlanningPlugin : PluginBase
    {
        public override bool UsesFilters { get { return true; } }

        public override bool UsesProjectSystem { get { return true; } }

        public override bool UsesWorkspaces { get { return true; } }

        public override async Task InitializeAsync()
        {
            var serviceLocator = this.GetServiceLocator();
            var typeFactory = serviceLocator.ResolveType<ITypeFactory>();

            GridViewSettings.ApproximateHeaderHeightCalculation = true;

            serviceLocator.ResolveType<IUiContextService<UiContext>>().UiContext = new UiContext();

            serviceLocator.RegisterType<global::Chameleon.Validation.IValidator, Validation.FactoryPlanningValidator>();
            serviceLocator.RegisterType<IDataGridScopeService, TabbedDataGridScopeService>();
            serviceLocator.RegisterType<IProductSuiteService, Services.ProductSuiteService>();
            //serviceLocator.RegisterType<IFilterCustomizationService, Gum.Filtering.FilterCustomizationService>();

            serviceLocator.RegisterType<IProjectInitializer, DirectoryProjectInitializer>();
            serviceLocator.RegisterType<IProjectReader, ProjectReader>();
            serviceLocator.RegisterType<IProjectWriter, ProjectWriter>();

            serviceLocator.RegisterTypeAndInstantiate<SaveProjectWatcher>();

            //serviceLocator.RegisterType<IWorkspaceInitializer, WorkspaceInitializer>();

            //serviceLocator.RegisterType<ICalculator, Calculator>();
            ////serviceLocator.RegisterType<ISequencer, Sequencer>();
            //serviceLocator.RegisterType<global::FactorySequencer.Validation.IValidator, Validator>();

            var commandManager = serviceLocator.ResolveType<ICommandManager>();
            commandManager.CreateCommandWithGesture(typeof(DataGridCommands.Data), nameof(DataGridCommands.Data.ExportDataToCsvFile));
            commandManager.CreateCommandWithGesture(typeof(DataGridCommands.Data), nameof(DataGridCommands.Data.ExportDataToCsvClipboard));
            // commandManager.CreateCommandWithGesture(typeof(DataGridCommands.Filtering), nameof(DataGridCommands.Filtering.ClearDataGridFilters));
            commandManager.CreateCommandWithGesture(typeof(DataGridCommands.Settings), nameof(DataGridCommands.Settings.ToggleTooltips));
            commandManager.CreateCommandWithGesture(typeof(DataGridCommands.Settings), nameof(DataGridCommands.Settings.ToggleQuickFilters));

            commandManager.CreateCommandWithGesture(typeof(SanitariumCommands.Tabs), nameof(SanitariumCommands.Tabs.Tear));
            commandManager.CreateCommandWithGesture(typeof(SanitariumCommands.Edit), nameof(SanitariumCommands.Edit.Production));
            commandManager.CreateCommandWithGesture(typeof(SanitariumCommands.Edit), nameof(SanitariumCommands.Edit.AddOrder));
            commandManager.CreateCommandWithGesture(typeof(SanitariumCommands.Edit), nameof(SanitariumCommands.Edit.Restore));
            commandManager.CreateCommandWithGesture(typeof(SanitariumCommands.Views), nameof(SanitariumCommands.Views.ShowInventory));
            commandManager.CreateCommandWithGesture(typeof(SanitariumCommands.Views), nameof(SanitariumCommands.Views.ShowResources));

            // Note: force load themes for now
            var application = Application.Current;
            application.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("/Gum.Ui.Apps.Controls;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
            });

            application.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("/SES.Projects.Sanitarium.Chameleon.FactoryPlanning;component/themes/generic.xaml", UriKind.RelativeOrAbsolute)
            });
        }

        public override async Task RegisterScopesAsync(IScopeManager scopeManager)
        {
            foreach (var fieldInfo in typeof(ScopeNames).GetFieldsEx(allowStaticMembers: true))
            {
                var scopeName = fieldInfo.GetValue(null) as string;
                if (!string.IsNullOrWhiteSpace(scopeName))
                {
                    await scopeManager.RegisterScopeAsync(scopeName);
                }
            }
        }

        public override async Task InitializeAfterShowingShellAsync()
        {
            var serviceLocator = this.GetServiceLocator();
            var tabService = serviceLocator.ResolveType<ITabService>();

            tabService.CreateAndAddDataGridTab<ItemsDataGridViewModel>("Items", ScopeNames.Items);
            tabService.CreateAndAddDataGridTab<BillOfMaterialRecordsDataGridViewModel>("BOM", ScopeNames.BillOfMaterials);
            tabService.CreateAndAddDataGridTab<ItemShortagesDataGridViewModel>("Item Shortages", ScopeNames.ItemShortages);
            tabService.CreateAndAddDataGridTab<ItemRoutingsDataGridViewModel>("Item Routes", ScopeNames.ItemRoutes);
            tabService.CreateAndAddDataGridTab<ResourceUtilisationsDataGridViewModel>("Resource Utilisation", ScopeNames.ResourceUtilisation);
            tabService.CreateAndAddDataGridTab<ItemInventoryMovementsDataGridViewModel>("Item Inventory Movements", ScopeNames.ItemInventoryMovements);
        }

        public override void CustomizeRibbon(Ribbon ribbon)
        {
            base.CustomizeRibbon(ribbon);

            var ribbonView = ribbon.FindOrCreateRibbonGroupBox(RibbonTabs.View.Name, RibbonTabs.View.Project.Name);
            ribbonView.Items.Add(new ExportImportRibbonView());

            ProjectViewConfiguration.IsProjectRibbonViewSaveButtonVisible = true;
            ProjectViewConfiguration.IsProjectRibbonViewRefreshButtonVisible = true;
        }

        public override FrameworkElement GetSettingsView()
        {
            return new SettingsView();
        }
    }
}
