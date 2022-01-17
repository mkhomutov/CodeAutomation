namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class Services
    {
        public static void Save()
        {
            var tabItems = Global.Config.CsvList.
                Select(x => $"var tabItem{x.ClassName} = tabService.CreateAndAddDataGridTab<{x.ClassName}DataGridViewModel>(\"{x.ClassName}\", ScopeNames.{x.ClassName}, false);").
                ToArray().
                JoinWithTabs(2);

            var viewModelsUsings = Global.Config.CsvList.
                Select(x => $"using {Global.Namespace}.UI.Tabs.{x.ClassName}.ViewModels;").
                ToArray().
                JoinWithTabs(0);

            var addTabItems = Global.Config.CsvList.
                Select(x => $"tabService.Add(tabItem{x.ClassName});").
                ToArray().
                JoinWithTabs(2);

            var firstTab = "tabItem" + Global.Config.CsvList.FirstOrDefault().ClassName;

            var applicationInitializationServiceContent = Template.GetByName("ApplicationInitializationService.cs").
                Replace("%VIEWMODELUSINGS%", viewModelsUsings).
                Replace("%TABITEMS%", tabItems).
                Replace("%ADDTABITEMS%", addTabItems).
                Replace("%FIRSTTAB%", firstTab);

            var ribbonServiceContent = Template.GetByName("RibbonService.cs");

            var applicationInitializationServiceFile = Path.Combine(Global.Path, "Services", "ApplicationInitializationService.cs");
            applicationInitializationServiceContent.AddCopyright("ApplicationInitializationService.cs").SaveToFile(applicationInitializationServiceFile);

            var ribbonServiceFile = Path.Combine(Global.Path, "UI", "Services", "RibbonService.cs");
            ribbonServiceContent.SaveToFile(ribbonServiceFile);
        }
    }
}
