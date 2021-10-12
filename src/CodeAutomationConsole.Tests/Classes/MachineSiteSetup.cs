namespace CodeAutomationConsole
{
    using System;

    public class MachineSiteSetup
    {
        #region Properties
        public string BranchCode { get; set; }
		public string IsValidRccp { get; set; }
		public string MachineAbbreviation { get; set; }
		public string IsValidMachine { get; set; }
		public string Comments { get; set; }
		public string MachineType { get; set; }
		public string MachineLocation { get; set; }
		public string Efficiency { get; set; }
		public string KilosPerBundle { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
        #endregion
    }
}