namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;

    public class Tabs : ViewTemplate
    {
        public Tabs(ViewTab tab) : base(Path.Combine(Global.Path, "UI", "Tabs", tab.RelatedClassName), $"{tab.RelatedClassName}DataGridView")
        {
            var addColumns = tab.GetDataGrid().Columns.Select(x =>
                {
                   var columnName = x.Title.Equals(tab.RelatedClassName) ? x.Title + "Property" : x.Title;
                   return $"allocatedDataGridConfiguration.AddColumn(x => x.{columnName});";
                }).
                ToArray().
                JoinWithTabs(2);

            ViewContent = Template.GetByName("[DataGridView].xaml").
                Replace("%PROJECTNAMESPACE%", Global.Namespace).
                Replace("%CLASSNAME%", tab.RelatedClassName);

            ViewCsContent = Template.GetByName("[DataGridView].xaml.cs").
                Replace("%PROJECTNAMESPACE%", Global.Namespace).
                Replace("%CLASSNAME%", tab.RelatedClassName);

            ViewModelContent = Template.GetByName("[DataGridViewModel].cs").
                Replace("%PROJECTNAMESPACE%", Global.Namespace).
                Replace("%CLASSNAME%", tab.RelatedClassName).
                Replace("%CSVNAME%", tab.FromCsv).
                Replace("%ADDCOLUMNS%", addColumns);
        }
    }
}
