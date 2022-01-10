namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ProjectManagement
    {
        public ProjectManagement(string ns, string projectPath, List<CsvListMember>csv)
        {
            ProjectPath = projectPath;

            var masterDataCollections = Global.Config.CsvList.Select(x => $"public IReadOnlyCollection<{x.ClassName}> {x.Name} {{ get; set; }}").
                ToArray().
                JoinWithTabs(2);

            var files = csv.Select(x => $"var {x.ClassName}File = Path.Combine(location, FileNames.{x.Name});").
                ToArray().
                JoinWithTabs(2);

            var projectMasterData = csv.Select(x => $"project.MasterData.{x.Name} = _projectSerializationService.LoadRecords<{x.ClassName}, {x.ClassName}Map>({x.ClassName}File);").
                ToArray().
                JoinWithTabs(2);

            Project = $@"
using Orc.ProjectManagement;
using {ns}.Data.Models;

namespace {ns}.Data.ProjectManagement;

public class Project : ProjectBase
{{
    public Project(string location) : this(location, string.Empty)
    {{
    }}

    public Project(string location, string title) : base(location, title)
    {{
        MasterData = new MasterData();
    }}

    public MasterData MasterData {{ get; }}
}}
";
            ProjectReader = $@"
using System.IO;
using System.Threading.Tasks;
using Catel;
using Catel.Configuration;
using Gum.ProjectManagement;
using Gum.Projects.Services;
using Orc.FileSystem;
using Orc.ProjectManagement;
using {ns}.Data.Models;
using {ns}.Data.Models.Maps;
using ProjectReaderBase = Gum.ProjectManagement.ProjectReaderBase;

namespace {ns}.Data.ProjectManagement;

public class ProjectReader : ProjectReaderBase
{{
    private readonly IProjectSerializationService _projectSerializationService;

    public ProjectReader(IDirectoryService directoryService, IFileService fileService, IProjectSettingsSerializationService projectSettingsSerializationService,
        IConfigurationService configurationService, IProjectSerializationService projectSerializationService)
        : base(directoryService, fileService, projectSettingsSerializationService, configurationService)
    {{
        Argument.IsNotNull(() => projectSerializationService);

        _projectSerializationService = projectSerializationService;
    }}

    protected override async Task<IProject> ReadFromLocationAsync(string location, ProjectSettings projectSettings)
    {{
        var project = new Project(location);

        {files}

        {projectMasterData}

        return project;
    }}
}}
";
            MasterData = $@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace {Global.Namespace}.Data.Models
{{
    public class MasterData
    {{
        {masterDataCollections}
    }}
}}
";
        }

        public string ProjectPath { get; set; }

        public string Project { get; set; }
        
        public string ProjectReader { get; set; }

        public string MasterData { get; set; }

        public void Save()
        {
            var projectFile = Path.Combine(ProjectPath, "Data", "ProjectManagement", "Project.cs");
            var projectReaderFile = Path.Combine(ProjectPath, "Data", "ProjectManagement", "ProjectReader.cs");
            var masterDataFile = Path.Combine(ProjectPath, "Data", "ProjectManagement", "MasterData.cs");

            Project.SaveToFile(projectFile);
            ProjectReader.SaveToFile(projectReaderFile);
            MasterData.SaveToFile(masterDataFile);
        }
    }
}
