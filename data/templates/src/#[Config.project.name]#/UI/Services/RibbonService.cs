using System.Windows;
using Orchestra.Services;
using #[$Namespace(Config.project.name)]#.UI.Views;

namespace #[$Namespace(Config.project.name)]#.UI.Services;

public class RibbonService : IRibbonService
{
    public FrameworkElement GetRibbon()
    {
        return new RibbonView();
    }

    public FrameworkElement GetMainView()
    {
        return new MainView();
    }

    public FrameworkElement GetStatusBar()
    {
        return new StatusBarView();
    }
}
