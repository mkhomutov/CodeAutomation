using System.Threading.Tasks;
using Catel.MVVM;
using Catel.Services;
using Gum;
using Gum.Commands;
using Orc.ProjectManagement;

namespace #[$Namespace(name)]#.Commands
{
    public class ProjectOpenCommandContainer : ProjectOpenAsDirectoryCommandContainerBase
    {
        #region Constructors
        public ProjectOpenCommandContainer(ICommandManager commandManager, IProjectManager projectManager,
            ISelectDirectoryService selectDirectoryService, IDispatcherService dispatcherService)
            : base(commandManager, projectManager, selectDirectoryService, dispatcherService)
        {
        }
        #endregion
    }
}
