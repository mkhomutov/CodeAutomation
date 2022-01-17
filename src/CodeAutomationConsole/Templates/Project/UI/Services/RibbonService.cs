using System.Windows;
using Orchestra.Services;
using %PROJECTNAMESPACE%.UI.Views;

namespace %PROJECTNAMESPACE%.UI.Services;

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
