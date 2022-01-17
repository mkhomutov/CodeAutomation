namespace CodeAutomationConsole
{
    using System.IO;
    using System.Linq;

    public class Tab : ViewTemplate
    {
        public Tab(CsvListMember csv) : base(Path.Combine(Global.Path, "UI", "Tabs", csv.ClassName), $"{csv.ClassName}DataGridView")
        {
            var addColumns = csv.Details.Select(x => {
                var columnName = x.Alias ?? x.Field;
                if (columnName.Equals(csv.ClassName)) { columnName += "Property"; }
                return $"allocatedDataGridConfiguration.AddColumn(x => x.{columnName});";
                }).
                ToArray().
                JoinWithTabs(2);

            ViewContent = Template.GetByName("CsvClassNameDataGridView.xaml").
                Replace("%PROJECTNAMESPACE%", Global.Namespace).
                Replace("%CLASSNAME%", csv.ClassName);

            ViewCsContent = Template.GetByName("CsvClassNameDataGridView.xaml.cs").
                Replace("%PROJECTNAMESPACE%", Global.Namespace).
                Replace("%CLASSNAME%", csv.ClassName);

            ViewModelContent = Template.GetByName("CsvClassNameDataGridViewModel.cs").
                Replace("%PROJECTNAMESPACE%", Global.Namespace).
                Replace("%CLASSNAME%", csv.ClassName).
                Replace("%ADDCOLUMNS%", addColumns);
        }
    }
}
