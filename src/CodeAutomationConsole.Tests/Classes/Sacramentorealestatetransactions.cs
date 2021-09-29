namespace CodeAutomationConsole
{
    using System;

    public class Sacramentorealestatetransactions
    {
        // Fields
        private readonly string _street;
		private readonly string _city;
		private readonly string _zip;
		private readonly string _state;
		private readonly string _beds;
		private readonly string _baths;
		private readonly string _sq__ft;
		private readonly string _type;
		private readonly string _sale_date;
		private readonly string _price;
		private readonly string _latitude;
		private readonly string _longitude;

        // Constructor
        public Sacramentorealestatetransactions(string street, string city, string zip, string state, string beds, string baths, string sq__ft, string type, string sale_date, string price, string latitude, string longitude)
        {
            _street = street;
			_city = city;
			_zip = zip;
			_state = state;
			_beds = beds;
			_baths = baths;
			_sq__ft = sq__ft;
			_type = type;
			_sale_date = sale_date;
			_price = price;
			_latitude = latitude;
			_longitude = longitude;
        }

        // Properties
        public string Street
		{
			get => _street;
		}
		public string City
		{
			get => _city;
		}
		public string Zip
		{
			get => _zip;
		}
		public string State
		{
			get => _state;
		}
		public string Beds
		{
			get => _beds;
		}
		public string Baths
		{
			get => _baths;
		}
		public string Sq__ft
		{
			get => _sq__ft;
		}
		public string Type
		{
			get => _type;
		}
		public string Sale_date
		{
			get => _sale_date;
		}
		public string Price
		{
			get => _price;
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
