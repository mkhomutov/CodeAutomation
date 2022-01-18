namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class Fluent
    {
        private readonly XNamespace _ns = "urn:fluent-ribbon";
        private readonly XNamespace _x = "http://schemas.microsoft.com/winfx/2006/xaml";

        public Fluent() { }

        public XNamespace Ns() { return _ns; }
        public XNamespace X() { return _x; }
    }
}
