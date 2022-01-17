namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class Default
    {
        private readonly XNamespace _ns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";

        public Default() { }

        public XNamespace Ns => _ns;
    }
}
