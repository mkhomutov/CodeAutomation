namespace CodeAutomationConsole
{
    using Orc.Csv;

    public class SacramentocrimeJanuary2006 : ClassMapBase<SacramentocrimeJanuary2006>
    {
        #region Constructors
        public SacramentocrimeJanuary2006()
        {
            Map(x => x.Cdatetime);
			Map(x => x.Address);
			Map(x => x.District);
			Map(x => x.Beat);
			Map(x => x.Grid);
			Map(x => x.Crimedescr);
			Map(x => x.Ucr_ncic_code);
			Map(x => x.Latitude);
			Map(x => x.Longitude);
        }
        #endregion
    }
}
