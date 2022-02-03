namespace CodeAutomationConsole;

public interface ISettingsProcessor
{
    public string Name { get; }
    AutomationSettings Run(AutomationSettings settings);
}