using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutomationConsole
{
    public static class ObjectExtension
    {
        public static Dictionary<object, object> ToDictionary(this object obj)
        {
            return (Dictionary<object, object>)(IEnumerable<KeyValuePair<object, object>>)obj;
        }
    }
}
