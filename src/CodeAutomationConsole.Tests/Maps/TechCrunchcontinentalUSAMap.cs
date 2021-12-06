namespace SES.Projects.GFG.Chameleon
{
    using Orc.Csv;

    public class TechCrunchcontinentalUSA : ClassMapBase<TechCrunchcontinentalUSA>
    {
        #region Constructors
        public TechCrunchcontinentalUSA()
        {
            Map(x => x.Permalink).Name("permalink");
			Map(x => x.Company).Name("company");
			Map(x => x.NumEmps).Name("numEmps");
			Map(x => x.Category).Name("category");
			Map(x => x.City).Name("city");
			Map(x => x.State).Name("state");
			Map(x => x.FundedDate).Name("fundedDate").AsDateTime();
			Map(x => x.RaisedAmt).Name("raisedAmt").AsDouble();
			Map(x => x.RaisedCurrency).Name("raisedCurrency");
			Map(x => x.Round).Name("round");
        }
        #endregion
    }
}
