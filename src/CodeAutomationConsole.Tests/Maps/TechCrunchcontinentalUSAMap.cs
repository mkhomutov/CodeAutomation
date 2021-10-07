namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class TechCrunchcontinentalUSA : ClassMapBase<TechCrunchcontinentalUSA>
    {
        #region Constructors
        public TechCrunchcontinentalUSA()
        {
            Map(x => x.Permalink);
			Map(x => x.Company);
			Map(x => x.NumEmps);
			Map(x => x.Category);
			Map(x => x.City);
			Map(x => x.State);
			Map(x => x.FundedDate);
			Map(x => x.RaisedAmt);
			Map(x => x.RaisedCurrency);
			Map(x => x.Round);
        }
        #endregion
    }
}
