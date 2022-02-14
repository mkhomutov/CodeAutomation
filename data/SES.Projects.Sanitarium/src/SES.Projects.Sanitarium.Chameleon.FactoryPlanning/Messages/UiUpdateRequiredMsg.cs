namespace SES.Projects.Sanitarium.Chameleon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement;

    public class UiUpdateRequiredMsg
    {
        public UiUpdateRequiredMsg(FactoryPlanningProject project)
        {
            Project = project;
        }

        public FactoryPlanningProject Project { get; }
    }
}
