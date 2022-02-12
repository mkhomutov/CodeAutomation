namespace CodeAutomationConsole
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public static class Services
    {
        public static void Save()
        {
            var tabs = Global.Config.GetProjectView("MainView").Tabs;

            var tabItems = tabs.Select(tab => $"var tabItem{tab.RelatedClassName} = tabService.CreateAndAddDataGridTab<{tab.RelatedClassName}DataGridViewModel>(\"{tab.Title}\", ScopeNames.{tab.RelatedClassName}, false);").
                ToArray().
                JoinWithTabs(2);

            var viewModelsUsings = tabs.Select(tab => $"using {Global.Namespace}.UI.Tabs.{tab.RelatedClassName}.ViewModels;").
                ToArray().
                JoinWithTabs(0);

            var addTabItems = tabs.Select(tab => $"tabService.Add(tabItem{tab.RelatedClassName});").
                ToArray().
                JoinWithTabs(2);

            var firstTab = "tabItem" + tabs.FirstOrDefault().RelatedClassName;

            var applicationInitializationServiceContent = CodeTemplate.GetByName("ApplicationInitializationService.cs").
                Replace("%VIEWMODELUSINGS%", viewModelsUsings).
                Replace("%TABITEMS%", tabItems).
                Replace("%ADDTABITEMS%", addTabItems).
                Replace("%FIRSTTAB%", firstTab);

            var ribbonServiceContent = CodeTemplate.GetByName("RibbonService.cs");

            var applicationInitializationServiceFile = Path.Combine(Global.Path, "Services", "ApplicationInitializationService.cs");
            applicationInitializationServiceContent.AddCopyright("ApplicationInitializationService.cs").SaveToFile(applicationInitializationServiceFile);

            var ribbonServiceFile = Path.Combine(Global.Path, "UI", "Services", "RibbonService.cs");
            ribbonServiceContent.SaveToFile(ribbonServiceFile);
        }
    }
}
