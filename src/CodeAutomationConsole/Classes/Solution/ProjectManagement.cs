namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;

    public static class ProjectManagement
    {
        public static void Save()
        {
            var masterDataCollections = Global.Config.CsvList.
                Select(x => $"public IReadOnlyCollection<{x.ClassName}> {x.File} {{ get; set; }}").
                ToArray().
                JoinWithTabs(2);

            var files = Global.Config.CsvList.
                Select(x => $"var {x.ClassName}File = Path.Combine(location, FileNames.{x.File});").
                ToArray().
                JoinWithTabs(2);

            var projectMasterData = Global.Config.CsvList.
                Select(x => $"project.MasterData.{x.File} = _projectSerializationService.LoadRecords<{x.ClassName}, {x.ClassName}Map>({x.ClassName}File);").
                ToArray().
                JoinWithTabs(2);

            var project = CodeTemplate.GetByName("Project.cs").
                Replace("%PROJECTNAMESPACE%", Global.Namespace);

            var projectReader = CodeTemplate.GetByName("ProjectReader.cs").
                Replace("%FILES%", files).
                Replace("%PROJECTMASTERDATA%", projectMasterData);

            var masterData = CodeTemplate.GetByName("MasterData.cs").
                Replace("%PROJECTNAMESPACE%", Global.Namespace).
                Replace("%MASTERDATACOLLECTIONS%", masterDataCollections);

            var projectFile = Path.Combine(Global.Path, "Data", "ProjectManagement", "Project.cs");
            project.SaveToFile(projectFile);

            var projectReaderFile = Path.Combine(Global.Path, "Data", "ProjectManagement", "ProjectReader.cs");
            projectReader.SaveToFile(projectReaderFile);

            var masterDataFile = Path.Combine(Global.Path, "Data", "ProjectManagement", "MasterData.cs");
            masterData.SaveToFile(masterDataFile);
        }
    }
}
