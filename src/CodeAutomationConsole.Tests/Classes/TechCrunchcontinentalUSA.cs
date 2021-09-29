namespace CodeAutomationConsole
{
    using System;

    public class TechCrunchcontinentalUSA
    {
        // Fields
        private readonly string _permalink;
		private readonly string _company;
		private readonly string _numEmps;
		private readonly string _category;
		private readonly string _city;
		private readonly string _state;
		private readonly string _fundedDate;
		private readonly string _raisedAmt;
		private readonly string _raisedCurrency;
		private readonly string _round;

        // Constructor
        public TechCrunchcontinentalUSA(string permalink, string company, string numEmps, string category, string city, string state, string fundedDate, string raisedAmt, string raisedCurrency, string round)
        {
            _permalink = permalink;
			_company = company;
			_numEmps = numEmps;
			_category = category;
			_city = city;
			_state = state;
			_fundedDate = fundedDate;
			_raisedAmt = raisedAmt;
			_raisedCurrency = raisedCurrency;
			_round = round;
        }

        // Properties
        public string Permalink
		{
			get => _permalink;
		}
		public string Company
		{
			get => _company;
		}
		public string NumEmps
		{
			get => _numEmps;
		}
		public string Category
		{
			get => _category;
		}
		public string City
		{
			get => _city;
		}
		public string State
		{
			get => _state;
		}
		public string FundedDate
		{
			get => _fundedDate;
		}
		public string RaisedAmt
		{
			get => _raisedAmt;
		}
		public string RaisedCurrency
		{
			get => _raisedCurrency;
		}
		public string Round
		{
			get => _round;
		}
    }
}
