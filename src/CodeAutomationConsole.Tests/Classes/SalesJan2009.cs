namespace CodeAutomationConsole
{
    using System;

    public class SalesJan2009
    {
        // Fields
        private readonly string _transaction_date;
		private readonly string _product;
		private readonly string _price;
		private readonly string _payment_Type;
		private readonly string _name;
		private readonly string _city;
		private readonly string _state;
		private readonly string _country;
		private readonly string _account_Created;
		private readonly string _last_Login;
		private readonly string _latitude;
		private readonly string _longitude;

        // Constructor
        public SalesJan2009(string transaction_date, string product, string price, string payment_Type, string name, string city, string state, string country, string account_Created, string last_Login, string latitude, string longitude)
        {
            _transaction_date = transaction_date;
			_product = product;
			_price = price;
			_payment_Type = payment_Type;
			_name = name;
			_city = city;
			_state = state;
			_country = country;
			_account_Created = account_Created;
			_last_Login = last_Login;
			_latitude = latitude;
			_longitude = longitude;
        }

        // Properties
        public string Transaction_date
		{
			get => _transaction_date;
		}
		public string Product
		{
			get => _product;
		}
		public string Price
		{
			get => _price;
		}
		public string Payment_Type
		{
			get => _payment_Type;
		}
		public string Name
		{
			get => _name;
		}
		public string City
		{
			get => _city;
		}
		public string State
		{
			get => _state;
		}
		public string Country
		{
			get => _country;
		}
		public string Account_Created
		{
			get => _account_Created;
		}
		public string Last_Login
		{
			get => _last_Login;
		}
		public string Latitude
		{
			get => _latitude;
		}
		public string Longitude
		{
			get => _longitude;
		}
    }
}
