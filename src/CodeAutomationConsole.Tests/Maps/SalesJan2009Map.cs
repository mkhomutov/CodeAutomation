namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class SalesJan2009 : ClassMapBase<SalesJan2009>
    {
        #region Constructors
        public SalesJan2009()
        {
            Map(x => x.Transaction_date);
			Map(x => x.Product);
			Map(x => x.Price);
			Map(x => x.Payment_Type);
			Map(x => x.Name);
			Map(x => x.City);
			Map(x => x.State);
			Map(x => x.Country);
			Map(x => x.Account_Created);
			Map(x => x.Last_Login);
			Map(x => x.Latitude);
			Map(x => x.Longitude);
        }
        #endregion
    }
}
