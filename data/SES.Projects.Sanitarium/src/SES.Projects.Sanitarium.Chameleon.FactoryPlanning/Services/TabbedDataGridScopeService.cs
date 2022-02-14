namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Services
{
    using System;
    using System.Linq;
    using System.Windows;
    using Catel;
    using Catel.Logging;
    using Catel.MVVM.Views;
    using Catel.Services;
    using Catel.Threading;
    using Catel.Windows;
    using Gum;
    using Gum.Controls;
    using Gum.Controls.Services;
    using Gum.Services;

    public class TabbedDataGridScopeService : IDataGridScopeService
    {
        private static readonly ILog Log = LogManager.GetCurrentClassLogger();

        private readonly ITabService _tabService;
        private readonly IViewManager _viewManager;
        private readonly IDispatcherService _dispatcherService;

        private string _currentScope;

        public TabbedDataGridScopeService(ITabService tabService, IViewManager viewManager,
            IDispatcherService dispatcherService)
        {
            Argument.IsNotNull(() => tabService);
            Argument.IsNotNull(() => viewManager);
            Argument.IsNotNull(() => dispatcherService);

            _tabService = tabService;
            _viewManager = viewManager;
            _dispatcherService = dispatcherService;
            _tabService.SelectedTabChanged += OnTabServiceSelectedTabChanged;
        }

        public virtual object GetScope()
        {
            return _currentScope;
        }

        private void DetermineScope()
        {
            string scope = null;

            var selectedTab = _tabService.SelectedTab;
            if (selectedTab is not null)
            {
                ScopeContext scopeContext = null;

                if (_viewManager.GetViewsOfViewModel(selectedTab.ViewModel).FirstOrDefault() is FrameworkElement view)
                {
                    var dg = view.FindVisualDescendantByType<DataGrid>();
                    if (dg is not null)
                    {
                        scopeContext = dg.Tag as ScopeContext;
                    }
                }
                else
                {
                    Log.Warning($"Could not find a view for tabbed view model '{selectedTab.ViewModel?.GetType()}', this could result in issues");
                }

                scope = scopeContext is null
                    ? selectedTab.Scope
                    : scopeContext.Key;
            }

            _currentScope = scope;
        }

        public event EventHandler<ScopeChangedEventArgs> ScopeChanged;

        private void OnTabServiceSelectedTabChanged(object sender, TabItemEventArgs e)
        {
#pragma warning disable AvoidAsyncVoid
            _dispatcherService.BeginInvoke(async () =>
#pragma warning restore AvoidAsyncVoid
            {
                // We need to give the view time to load
                await TaskShim.Delay(25);

                var oldScope = _currentScope;
                DetermineScope();
                var newScope = _currentScope;

                ScopeChanged?.Invoke(this, new ScopeChangedEventArgs(oldScope, newScope));
            });
        }
    }
}
