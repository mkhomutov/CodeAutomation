namespace #[$Namespace(name)]#;

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using Catel.IoC;
using Catel.Logging;
using Catel.Services;
using Orchestra.Services;
using Orchestra.Views;
using Gum.Controls;
using Gum.Services;

public partial class App : Application
{
    #region Constants
    #endregion

    #region Fields
    #endregion

    #region Constructors
    public App()
    {

    }
    #endregion

    #region Methods
    protected override async void OnStartup(StartupEventArgs e)
    {
#if DEBUG
        LogManager.AddDebugListener(true);
#endif

        GridViewSettings.ApproximateHeaderHeightCalculation = true;

        var serviceLocator = ServiceLocator.Default;
        OpaqueAccentColorHelper.CreateOpaqueAccentColorResourceDictionary();

        var shellService = serviceLocator.ResolveType<IShellService>();
        await shellService.CreateAsync<ShellWindow>();
    }
    #endregion
}
