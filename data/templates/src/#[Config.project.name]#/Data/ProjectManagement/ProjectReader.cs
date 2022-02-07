using System.IO;
using System.Threading.Tasks;
using Catel;
using Catel.Configuration;
using Gum.ProjectManagement;
using Gum.Projects.Services;
using Orc.FileSystem;
using Orc.ProjectManagement;
using #[$Namespace(Config.project.name)]#.Data.Models;
using #[$Namespace(Config.project.name)]#.Data.Models.Maps;
using ProjectReaderBase = Gum.ProjectManagement.ProjectReaderBase;

namespace #[$Namespace(Config.project.name)]#.Data.ProjectManagement;

public class ProjectReader : ProjectReaderBase
{
    private readonly IProjectSerializationService _projectSerializationService;

    public ProjectReader(IDirectoryService directoryService, IFileService fileService, IProjectSettingsSerializationService projectSettingsSerializationService,
        IConfigurationService configurationService, IProjectSerializationService projectSerializationService)
        : base(directoryService, fileService, projectSettingsSerializationService, configurationService)
    {
        Argument.IsNotNull(() => projectSerializationService);

        _projectSerializationService = projectSerializationService;
    }

    protected override async Task<IProject> ReadFromLocationAsync(string location, ProjectSettings projectSettings)
    {
        var project = new Project(location);

		#[$MasterData().loadRecords]#

        return project;
    }
}
