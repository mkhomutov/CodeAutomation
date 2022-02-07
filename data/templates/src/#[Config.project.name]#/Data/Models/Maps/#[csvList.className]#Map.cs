using Orc.Csv;

namespace #[$Namespace(Config.project.name)]#.Data.Models.Maps;

public sealed class #[className]#Map : ClassMapBase<#[className]#>
{
    #region Constructors
    public #[className]#Map()
    {
		#[$CsvMap().ctor]#
    }
    #endregion
}
