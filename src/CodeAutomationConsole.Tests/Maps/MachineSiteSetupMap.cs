namespace SES.Projects.GFG.Chameleon
{
    using Orc.Csv;

    public class MachineSiteSetup : ClassMapBase<MachineSiteSetup>
    {
        #region Constructors
        public MachineSiteSetup()
        {
            Map(x => x.BranchCode).Name("BranchCode").AsDouble();
			Map(x => x.RCCPValid).Name("RCCPValid").AsBool();
			Map(x => x.MachAbrev).Name("MachAbrev");
			Map(x => x.ValidMC).Name("ValidMC").AsBool();
			Map(x => x.Comments).Name("Comments");
			Map(x => x.MachineType).Name("MachineType");
			Map(x => x.MCLoc).Name("MCLoc");
			Map(x => x.LocalEfficiency).Name("LocalEfficiency").AsDouble();
			Map(x => x.BndlKg).Name("BndlKg");
			Map(x => x.StartTime).Name("StartTime");
			Map(x => x.EndTime).Name("EndTime");
        }
        #endregion
    }
}
