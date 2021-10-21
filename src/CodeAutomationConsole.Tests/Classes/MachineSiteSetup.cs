namespace SES.Projects.GFG.Chameleon
{
    using System;

    public class MachineSiteSetup
    {
        #region Properties
        public double BranchCode { get; set; }
		public bool RCCPValid { get; set; }
		public string MachAbrev { get; set; }
		public bool ValidMC { get; set; }
		public string Comments { get; set; }
		public string MachineType { get; set; }
		public string MCLoc { get; set; }
		public double LocalEfficiency { get; set; }
		public string BndlKg { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
        #endregion
    }
}
