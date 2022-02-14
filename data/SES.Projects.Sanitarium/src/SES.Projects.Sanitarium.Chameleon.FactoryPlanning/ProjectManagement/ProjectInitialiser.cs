

namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using SES.Projects.Sanitarium.Chameleon.FactoryPlanning;
    using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Models;

    public static class ProjectInitialiser
    {
        public static void Initialise(this FactoryPlanningProject project)
        {
            // NOTE: In most projects we should also validate data first

            project.InitialiseData();
        }

        public static void InitialiseData(this FactoryPlanningProject project)
        {
            // TODO: implement data initialization here
        }
    }
}
