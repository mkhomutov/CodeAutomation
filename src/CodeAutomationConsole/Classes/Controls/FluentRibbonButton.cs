namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FluentRibbonButton
    {
        public FluentRibbonButton(string header, string largeIcon, string command) : this(header, largeIcon)
        {
            Command = command;
        }

        public FluentRibbonButton(string header, string largeIcon)
        {
            Header = header;
            LargeIcon = largeIcon;
        }

        public FluentRibbonButton() { }

        public string Header { get; set; }
        public string LargeIcon { get; set; }
        public string Command { get; set; }
    }
}
