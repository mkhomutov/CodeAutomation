using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutomationConsole
{
    public static class CustomFunctions
    {
        private static readonly Dictionary<string, string> _guids = new Dictionary<string, string>();

        public static string Guid(string key)
        {
            if (!_guids.TryGetValue(key, out var guid))
            {
                guid = System.Guid.NewGuid().ToString().ToUpper();
                _guids[key] = guid;
            }

            return guid;
        }
    }
}
