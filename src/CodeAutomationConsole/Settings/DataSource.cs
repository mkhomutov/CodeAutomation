namespace CodeAutomationConsole;

public class DataSource
{
    public string SettingsProcessor { get; set; }
    public string DataSourceType { get; set; } // CSV, DataBase, Excel
    public string Culture { get; set; }
    public string ConnectionString { get; set; }
}