using Orc.ProjectManagement;
using #[$Namespace(Config.project.name)]#.Data.Models;

namespace #[$Namespace(Config.project.name)]#.Data.ProjectManagement;

public class Project : ProjectBase
{
    public Project(string location) : this(location, string.Empty)
    {
    }

    public Project(string location, string title) : base(location, title)
    {
        MasterData = new MasterData();
    }

    public MasterData MasterData { get; }
}
