namespace CodeAutomationConsole;

public class SettingsProcessor : ISettingsProcessor
{
    public string Name => throw new System.NotImplementedException();

    public AutomationSettings Run(AutomationSettings settings)
    {
        // TODO: implement scv files analyzing, merging settings and prepare settings for generating the code

        return settings;
    }
}