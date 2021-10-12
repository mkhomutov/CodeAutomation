namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class SalesJan2009 : ClassMapBase<SalesJan2009>
    {
        #region Constructors
        public SalesJan2009()
        {
            Map(x => x.Transaction_date).Name("Transaction_date");
			Map(x => x.Product).Name("Product");
			Map(x => x.Price).Name("Price");
			Map(x => x.Payment_Type).Name("Payment_Type");
			Map(x => x.Name).Name("Name");
			Map(x => x.City).Name("City");
			Map(x => x.State).Name("State");
			Map(x => x.Country).Name("Country");
			Map(x => x.Account_Created).Name("Account_Created");
			Map(x => x.Last_Login).Name("Last_Login");
			Map(x => x.Latitude).Name("Latitude");
			Map(x => x.Longitude).Name("Longitude");
        }
        #endregion
    }
}
