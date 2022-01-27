using Catel.MVVM;

namespace %PROJECTNAMESPACE%.UI.ViewModels;

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
