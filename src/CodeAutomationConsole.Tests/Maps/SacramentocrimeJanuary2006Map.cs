namespace SES.Projects.GFG.Chameleon
{
    using Orc.Csv;

    public class SacramentocrimeJanuary2006 : ClassMapBase<SacramentocrimeJanuary2006>
    {
        #region Constructors
        public SacramentocrimeJanuary2006()
        {
            Map(x => x.Cdatetime).Name("cdatetime").AsDateTime();
			Map(x => x.Address).Name("address");
			Map(x => x.District).Name("district").AsDouble();
			Map(x => x.Beat).Name("beat");
			Map(x => x.Grid).Name("grid").AsDouble();
			Map(x => x.Crimedescr).Name("crimedescr");
			Map(x => x.UcrNcicCode).Name("ucr_ncic_code").AsDouble();
			Map(x => x.Latitude).Name("latitude").AsDouble();
			Map(x => x.Longitude).Name("longitude").AsDouble();
        }
        #endregion
    }
}
