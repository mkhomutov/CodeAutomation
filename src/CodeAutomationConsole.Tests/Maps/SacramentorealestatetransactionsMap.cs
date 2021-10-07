namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class Sacramentorealestatetransactions : ClassMapBase<Sacramentorealestatetransactions>
    {
        #region Constructors
        public Sacramentorealestatetransactions()
        {
            Map(x => x.Street);
			Map(x => x.City);
			Map(x => x.Zip);
			Map(x => x.State);
			Map(x => x.Beds);
			Map(x => x.Baths);
			Map(x => x.Sq__ft);
			Map(x => x.Type);
			Map(x => x.Sale_date);
			Map(x => x.Price);
			Map(x => x.Latitude);
			Map(x => x.Longitude);
        }
        #endregion
    }
}
