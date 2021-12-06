namespace SES.Projects.GFG.Chameleon
{
    using Orc.Csv;

    public class Sacramentorealestatetransaction : ClassMapBase<Sacramentorealestatetransaction>
    {
        #region Constructors
        public Sacramentorealestatetransaction()
        {
            Map(x => x.Street).Name("street");
			Map(x => x.City).Name("city");
			Map(x => x.Zip).Name("zip").AsDouble();
			Map(x => x.State).Name("state");
			Map(x => x.Beds).Name("beds").AsDouble();
			Map(x => x.Baths).Name("baths").AsDouble();
			Map(x => x.SqFt).Name("sq__ft").AsDouble();
			Map(x => x.Type).Name("type");
			Map(x => x.SaleDate).Name("sale_date");
			Map(x => x.Price).Name("price").AsDouble();
			Map(x => x.Latitude).Name("latitude").AsDouble();
			Map(x => x.Longitude).Name("longitude").AsDouble();
        }
        #endregion
    }
}
