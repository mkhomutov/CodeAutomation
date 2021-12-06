namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FluentRibbonGroupBox
    {
        public FluentRibbonGroupBox(string header, List<FluentRibbonButton> buttons) : this(header)
        {
            Buttons = buttons;
        }

        public FluentRibbonGroupBox(string header) : this()
        {
            Header = header;
        }

        public FluentRibbonGroupBox()
        {
            Buttons = new List<FluentRibbonButton>();
        }

        public string Header { get; set; }

        public List<FluentRibbonButton> Buttons { get; set; }
    }
}
