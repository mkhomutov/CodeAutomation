using System.Collections.Generic;

namespace CodeAutomationConsole;

public class ScriptsSettings
{
    public string Tool { get; set; }
    public List<ToolCommand> Commands { get; set; }
}

public class ToolCommand
{
    public string Command { get; set; }
    public string WorkingFolder { get; set; }
}