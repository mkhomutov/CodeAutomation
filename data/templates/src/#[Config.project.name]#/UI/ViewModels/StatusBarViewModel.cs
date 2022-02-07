using Catel.MVVM;

namespace #[$Namespace(Config.project.name)]#.UI.ViewModels;

public class StatusBarViewModel : ViewModelBase
{
    #region Properties
    public override string Title
    {
        get { return "Status bar title binding"; }
    }
    #endregion

    public bool EnableAutomaticUpdates { get; set; }
}
