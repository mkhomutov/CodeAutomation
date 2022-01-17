using Orc.Csv;

namespace %PROJECTNAMESPACE%.Data.Models.Maps;

public sealed class %CLASSNAME%Map : ClassMapBase<%CLASSNAME%>
{
    #region Constructors
    public %CLASSNAME%Map()
    {
        // Map(x => x.PropertyByFieldName).Name("CsvField").AsString();
        %MAPPINGS%
    }
    #endregion
}
