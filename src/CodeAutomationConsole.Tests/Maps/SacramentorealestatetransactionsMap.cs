namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class Sacramentorealestatetransactions : ClassMapBase<Sacramentorealestatetransactions>
    {
        #region Constructors
        public Sacramentorealestatetransactions()
        {
            Map(x => x.Street).Name("Street");
			Map(x => x.City).Name("City");
			Map(x => x.Zip).Name("Zip");
			Map(x => x.State).Name("State");
			Map(x => x.Beds).Name("Beds");
			Map(x => x.Baths).Name("Baths");
			Map(x => x.Sq__ft).Name("Sq__ft");
			Map(x => x.Type).Name("Type");
			Map(x => x.Sale_date).Name("Sale_date");
			Map(x => x.Price).Name("Price");
			Map(x => x.Latitude).Name("Latitude");
			Map(x => x.Longitude).Name("Longitude");
        }
        #endregion
    }
}
