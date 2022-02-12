using Catel.IoC;
using Catel.MVVM;
using Gum;
using Gum.Drawing;
using Orchestra.Services;
using #[$Namespace(name)]#.Services;
using #[$Namespace(name)]#.UI.Services;

namespace #[$Namespace(name)]#;

public static class ModuleInitializer
{
    public static void Initialize()
    {
        Figure.IsErasingRequiredOnStylePropertyChanged = true;

        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<ICommandManager, CommandManager>();
        serviceLocator.RegisterType<ApplicationInfo, ProjectApplicationInfo>();
        serviceLocator.RegisterType<IRibbonService, RibbonService>();
        serviceLocator.RegisterType<IApplicationInitializationService, ApplicationInitializationService>();

        // ***** IMPORTANT NOTE *****
        //
        // Only register the shell services in the ModuleInitializer. All other types must be registered
        // in the ApplicationInitializationService
    }
}
