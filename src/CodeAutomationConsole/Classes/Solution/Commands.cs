namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Commands
    {
        public Commands(string ns, string projectPath)
        {
            ProjectPath = projectPath;

            ProjectOpenCommandContainer = @$"
using System.Threading.Tasks;
using Catel.MVVM;
using Catel.Services;
using Gum;
using Gum.Commands;
using Orc.ProjectManagement;

namespace {Global.Namespace}.Commands
{{
    public class ProjectOpenCommandContainer : ProjectOpenAsDirectoryCommandContainerBase
    {{
        #region Constructors
        public ProjectOpenCommandContainer(ICommandManager commandManager, IProjectManager projectManager,
            ISelectDirectoryService selectDirectoryService, IDispatcherService dispatcherService)
            : base(commandManager, projectManager, selectDirectoryService, dispatcherService)
        {{
        }}
        #endregion
    }}
}}";
        }
        public string ProjectPath { get; set; }
        public string ProjectOpenCommandContainer { get; set; }
        public void Save()
        {
            var file = Path.Combine(ProjectPath, "Commands", "ProjectOpenCommandContainer.cs");

            ProjectOpenCommandContainer.AddCopyright("ProjectOpenCommandContainer.cs").SaveToFile(file);
        }
    }
}
