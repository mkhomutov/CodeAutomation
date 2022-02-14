namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    internal sealed class ResourceUtilisationMap : Orc.Csv.ClassMapBase<ResourceUtilisation>
    {
        public ResourceUtilisationMap()
        {
            // Map booleans using .AsBool()
            // Map date/times using .AsDateTime() or .AsNullableDateTime()

            Map(m => m.Location).Name("#LOC", "LOC");
            Map(m => m.Resource).Name("RES");
            Map(m => m.StartDate).Name("STARTDATE");
            Map(m => m.Duration).Name("DUR");
            Map(m => m.TotalCapacity).Name("CPPTOTCAP");
            Map(m => m.TotalLoad).Name("MPTOTLOAD");
            Map(m => m.AvailableCapactiy).Name("AVAILCAP");
            Map(m => m.PercentCapacityUsed).Name("PCTUSED");
            Map(m => m.RegularCapacity).Name("MPREGULARCAP");
            Map(m => m.RegularLoad).Name("MPREGULARLOAD");
            Map(m => m.OvertimeCapacity).Name("MPOVERTIMECAP");
            Map(m => m.OvertimeLoad).Name("MPOVERTIMELOAD");
        }
    }
}
