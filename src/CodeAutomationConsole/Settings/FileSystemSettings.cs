using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeAutomationConsole
{
    public class FileSystemSettings
    {
        public string UpdateableFileMask { get; set; }
        public List<ExtensionSettings> Extensions { get; set; }
    }
}
