namespace SES.Projects.GFG.Chameleon
{
    using Orc.Csv;

    public class SalesJan2009 : ClassMapBase<SalesJan2009>
    {
        #region Constructors
        public SalesJan2009()
        {
            Map(x => x.TransactionDate).Name("Transaction_date").AsDateTime();
			Map(x => x.Product).Name("Product");
			Map(x => x.Price).Name("Price");
			Map(x => x.PaymentType).Name("Payment_Type");
			Map(x => x.Name).Name("Name");
			Map(x => x.City).Name("City");
			Map(x => x.State).Name("State");
			Map(x => x.Country).Name("Country");
			Map(x => x.AccountCreated).Name("Account_Created").AsDateTime();
			Map(x => x.LastLogin).Name("Last_Login").AsDateTime();
			Map(x => x.Latitude).Name("Latitude").AsDouble();
			Map(x => x.Longitude).Name("Longitude").AsDouble();
        }
        #endregion
    }
}
