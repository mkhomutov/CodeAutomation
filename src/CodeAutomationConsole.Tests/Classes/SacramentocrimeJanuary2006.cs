namespace CodeAutomationConsole
{
    using System;

    public class SacramentocrimeJanuary2006
    {
        // Fields
        private readonly string _cdatetime;
		private readonly string _address;
		private readonly string _district;
		private readonly string _beat;
		private readonly string _grid;
		private readonly string _crimedescr;
		private readonly string _ucr_ncic_code;
		private readonly string _latitude;
		private readonly string _longitude;

        // Constructor
        public SacramentocrimeJanuary2006(string cdatetime, string address, string district, string beat, string grid, string crimedescr, string ucr_ncic_code, string latitude, string longitude)
        {
            _cdatetime = cdatetime;
			_address = address;
			_district = district;
			_beat = beat;
			_grid = grid;
			_crimedescr = crimedescr;
			_ucr_ncic_code = ucr_ncic_code;
			_latitude = latitude;
			_longitude = longitude;
        }

        // Properties
        public string Cdatetime
		{
			get => _cdatetime;
		}
		public string Address
		{
			get => _address;
		}
		public string District
		{
			get => _district;
		}
		public string Beat
		{
			get => _beat;
		}
		public string Grid
		{
			get => _grid;
		}
		public string Crimedescr
		{
			get => _crimedescr;
		}
		public string Ucr_ncic_code
		{
			get => _ucr_ncic_code;
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
