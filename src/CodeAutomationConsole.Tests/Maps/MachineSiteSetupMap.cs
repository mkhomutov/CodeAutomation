namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class MachineSiteSetup : ClassMapBase<MachineSiteSetup>
    {
        #region Constructors
        public MachineSiteSetup()
        {
            Map(x => x.BranchCode).Name("BranchCode");
			Map(x => x.IsValidRccp).Name("RCCPValid").AsBool();
			Map(x => x.MachineAbbreviation).Name("MachAbrev");
			Map(x => x.IsValidMachine).Name("ValidMC").AsBool();
			Map(x => x.Comments).Name("Comments");
			Map(x => x.MachineType).Name("MachineType");
			Map(x => x.MachineLocation).Name("MCLoc");
			Map(x => x.Efficiency).Name("LocalEfficiency").Default(0);
			Map(x => x.KilosPerBundle).Name("BndlKg");
			Map(x => x.StartTime).Name("StartTime");
			Map(x => x.EndTime).Name("EndTime");
        }
        #endregion
    }
}