using Catel.Fody;
using Catel.MVVM;
using Catel.Reflection;
using %PROJECTNAMESPACE%.UI.Models;

namespace %PROJECTNAMESPACE%.UI.ViewModels;

public sealed class RibbonViewModel : ViewModelBase
{
    public RibbonViewModel()
    {
        var assembly = AssemblyHelper.GetEntryAssembly();
        Title = assembly.Title();

        ProjectViewConfiguration = new ProjectViewConfiguration()
        {
            IsOpenProjectBackstageTabItemVisible = true,
            IsOpenProjectViewVisible = true,
            IsSaveProjectBackstageButtonVisible = false,
            IsSaveAsProjectBackstageButtonVisible = false,
            IsCloseProjectBackstageButtonVisible = false,
        };
    }

    [Model]
    [Expose(nameof(Models.ProjectViewConfiguration.IsOpenProjectBackstageTabItemVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsOpenProjectViewVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsSaveProjectBackstageButtonVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsSaveAsProjectBackstageButtonVisible))]
    [Expose(nameof(Models.ProjectViewConfiguration.IsCloseProjectBackstageButtonVisible))]
    public ProjectViewConfiguration ProjectViewConfiguration { get; set; }
}
