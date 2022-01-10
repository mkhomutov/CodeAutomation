namespace CodeAutomationConsole
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    public class Tab : ViewTemplate
    {
        public Tab(CsvListMember csv) : base(Path.Combine(Global.Path, "UI", "Tabs", csv.ClassName), $"{csv.ClassName}DataGridView")
        {
            var name = csv.ClassName;

            var xml = new XElement(Catel + "UserControl",
                new XAttribute(X + "Class", $"{Global.Namespace}.UI.Tabs.{name}.Views.{ViewName}"),
                new XAttribute("xmlns", Default),
                new XAttribute(XNamespace.Xmlns + "x", X),
                new XAttribute(XNamespace.Xmlns + "mc", Mc),
                new XAttribute(XNamespace.Xmlns + "d", D),
                new XAttribute(XNamespace.Xmlns + "catel", Catel),
                new XAttribute(XNamespace.Xmlns + "gumprojects", GumProjects),
                new XAttribute(Mc + "Ignorable", "d"),
                new XAttribute(D + "DesignHeight", "450"),
                new XAttribute(D + "DesignWidth", "800"),
                new XElement(Default + "Grid",
                    new XElement(GumProjects + "DataGridEx",
                        new XAttribute(X + "Name", "dataGrid"),
                        new XAttribute("DataGridInteraction", "{Binding Interaction, Mode=OneWayToSource}"),
                        new XAttribute("ItemsSource", "{Binding Records}"),
                        new XAttribute("SelectionMode", "ExtendedRowSelection"),
                        new XAttribute("IsEditEnabled", "False")
                        )
                    )
                );

            var columns = csv.Details.Select(x => {
                var columnName = x.Alias ?? x.Field;
                if (columnName.Equals(csv.ClassName)) { columnName += "Property"; }
                return $"allocatedDataGridConfiguration.AddColumn(x => x.{columnName});";
                }).
                ToArray().
                JoinWithTabs(2);

            ViewContent = xml.ToString().FormatXaml();

            ViewCsContent = @$"using System;
using {Global.Namespace}.UI.Tabs.{name}.ViewModels;

namespace {Global.Namespace}.UI.Tabs.{name}.Views;

public partial class {name}DataGridView
{{
    public {name}DataGridView()
    {{
        InitializeComponent();
    }}
}}
";
            ViewModelContent = $@"using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Catel.Configuration;
using Catel.IoC;
using Catel.Linq;
using Catel.Services;
using Gum;
using Gum.Controls;
using Gum.Controls.Adapters;
using Gum.Controls.Services;
using Gum.Controls.ViewModels;
using Gum.Projects;
using Gum.Projects.Controls.Configuration;
using Gum.Services;
using Orc.FilterBuilder;
using Orc.ProjectManagement;
using Orc.Theming;
using {Global.Namespace}.Data.ProjectManagement;

namespace {Global.Namespace}.UI.Tabs.{name}.ViewModels;

public class {name}DataGridViewModel : IntegratedDataGridViewModelBase<Project, Data.Models.{name}>
{{
    private readonly DataGridConfiguration<Data.Models.{name}> _defaultConfiguration =
        CreateDataGridConfiguration();

    public {name}DataGridViewModel(DataGridContext context, IProjectManager projectManager,
        IProjectStateService projectStateService,
        IFilterService filterService, IDispatcherService dispatcherService,
        IProjectDataGridService projectDataGridService, ITabService tabService,
        IAccentColorService accentColorService, IConfigurationService configurationService, IScopeManager scopeManager,
        IServiceLocator serviceLocator)
        : base(context, projectManager, projectStateService, filterService, dispatcherService, projectDataGridService,
            tabService, accentColorService,
            configurationService, scopeManager, serviceLocator)
    {{
    }}

    protected override Task InitializeAsync()
    {{
        return base.InitializeAsync();
    }}

    protected override Task CloseAsync()
    {{
        return base.CloseAsync();
    }}

    protected override void OnInteractionUpdated(IDataGridInteraction oldInteraction, IDataGridInteraction newInteraction)
    {{
        base.OnInteractionUpdated(oldInteraction, newInteraction);
    }}

    protected override IPropertyProvider GetPropertyProvider() => _defaultConfiguration.PropertyProvider;
    protected override ISettingsAdapter GetSettingsAdapter() => _defaultConfiguration.SettingsAdapter;

    protected override string GetTabScope()
    {{
        return ScopeNames.{name};
    }}

    protected override async Task<IList<Data.Models.{name}>> GetProjectRecordsAsync(Project project)
    {{
        return project?.MasterData.{csv.Name}.ToList() ?? new List<Data.Models.{name}>(0);
    }}

    protected override async void InitializeDataGrid(IDataGridInteraction interaction)
    {{
        base.InitializeDataGrid(interaction);

        interaction.ApplyDefaultDataGridConfiguration(_defaultConfiguration);
    }}

    private static DataGridConfiguration<Data.Models.{name}> CreateDataGridConfiguration()
    {{
        var allocatedDataGridConfiguration = new DataGridConfiguration<Data.Models.{name}>()
            .SummaryRowLocation(RowGroupDetailsMode.Top)
            .StickyRowHeaders(true);

        {columns}

        return allocatedDataGridConfiguration;
    }}

}}

";

        }

    }
}
