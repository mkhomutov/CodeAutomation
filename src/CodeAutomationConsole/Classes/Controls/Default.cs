namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class Default
    {
        private readonly XNamespace _ns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";

        public Default() { }

        public XNamespace Ns() => _ns;
    }
}
