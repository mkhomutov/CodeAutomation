using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace #[$Namespace(Config.project.name)]#.Data.Models
{
    public class MasterData
    {
		#region Constructors
		public MasterData()
		{
			#[$MasterData().ctor]#
		}
		#endregion

        #region Properties
		#[$MasterData().properties]#
		#endregion
    }
}
