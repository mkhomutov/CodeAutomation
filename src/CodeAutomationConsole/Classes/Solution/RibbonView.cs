namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;

    public class RibbonView : ViewTemplate
    {
        public RibbonView() : base(Path.Combine(Global.Path, "UI"), "RibbonView")
        {
            //var ribbonMainView = Global.Config.GetRibbon("RibbonView");

            //var ribbonViewTabs = ribbonMainView.FluentRibbonTabs.
            //    Select(tab => tab.GetXml().ToString()).
            //    JoinWithTabs(0).
            //    RemoveXmlns().
            //    FormatXaml().
            //    Tabulate(3);

            ViewContent = Template.GetByName("RibbonView.xaml").
                //Replace("%FLUENTRIBBONTABS%", ribbonViewTabs).
                Replace("%PROJECTNAME%", Global.ProjectName.ToLower());

            ViewCsContent = Template.GetByName("RibbonView.xaml.cs");

            ViewModelContent = Template.GetByName("RibbonViewModel.cs");
        }
    }
}
