namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class SacramentocrimeJanuary2006 : ClassMapBase<SacramentocrimeJanuary2006>
    {
        #region Constructors
        public SacramentocrimeJanuary2006()
        {
            Map(x => x.Cdatetime).Name("Cdatetime");
			Map(x => x.Address).Name("Address");
			Map(x => x.District).Name("District");
			Map(x => x.Beat).Name("Beat");
			Map(x => x.Grid).Name("Grid");
			Map(x => x.Crimedescr).Name("Crimedescr");
			Map(x => x.Ucr_ncic_code).Name("Ucr_ncic_code");
			Map(x => x.Latitude).Name("Latitude");
			Map(x => x.Longitude).Name("Longitude");
        }
        #endregion
    }
}
