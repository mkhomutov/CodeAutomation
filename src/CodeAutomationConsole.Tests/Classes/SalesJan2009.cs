namespace SES.Projects.GFG.Chameleon
{
    using System;

    public class SalesJan2009
    {
        #region Properties
        public DateTime TransactionDate { get; set; }
		public string Product { get; set; }
		public string Price { get; set; }
		public string PaymentType { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public DateTime AccountCreated { get; set; }
		public DateTime LastLogin { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
        #endregion
    }
}
