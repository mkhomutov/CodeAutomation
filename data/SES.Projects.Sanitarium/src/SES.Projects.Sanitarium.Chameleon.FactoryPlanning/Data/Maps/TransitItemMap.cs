
namespace SES.Projects.Sanitarium.Chameleon.FactoryPlanning.ProjectManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SES.Projects.Sanitarium.Chameleon.FactoryPlanning.Models;

    internal sealed class TransitItemMap : Orc.Csv.ClassMapBase<TransitItem>
    {
        public TransitItemMap()
        {
            Map(x => x.Location).Name("DEST");
            Map(x => x.ItemId).Name("ITEM");
            Map(x => x.ArrivalDate).Name("ARRIVDATE");
            Map(x => x.Quantity).Name("QTY");
        }
    }
}
