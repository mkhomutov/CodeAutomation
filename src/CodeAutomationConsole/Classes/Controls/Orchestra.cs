namespace CodeAutomationConsole
{
    using System.Xml.Linq;

    public class Orchestra
    {
        private readonly XNamespace _ns = "http://schemas.wildgums.com/orchestra";

        public Orchestra() { }

        public XNamespace Ns() => _ns;
    }
}
