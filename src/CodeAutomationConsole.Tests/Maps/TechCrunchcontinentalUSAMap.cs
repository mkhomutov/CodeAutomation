namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class TechCrunchcontinentalUSA : ClassMapBase<TechCrunchcontinentalUSA>
    {
        #region Constructors
        public TechCrunchcontinentalUSA()
        {
            Map(x => x.Permalink).Name("Permalink");
			Map(x => x.Company).Name("Company");
			Map(x => x.NumEmps).Name("NumEmps");
			Map(x => x.Category).Name("Category");
			Map(x => x.City).Name("City");
			Map(x => x.State).Name("State");
			Map(x => x.FundedDate).Name("FundedDate");
			Map(x => x.RaisedAmt).Name("RaisedAmt");
			Map(x => x.RaisedCurrency).Name("RaisedCurrency");
			Map(x => x.Round).Name("Round");
        }
        #endregion
    }
}
