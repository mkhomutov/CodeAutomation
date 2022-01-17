// --------------------------------------------------------------------------------------------------------------------
// <copyright file="%CLASSNAME%DataGridViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2021 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System.Collections.Generic;
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
using %PROJECTNAMESPACE%.Data.ProjectManagement;

namespace %PROJECTNAMESPACE%.UI.Tabs.%CLASSNAME%.ViewModels;

public class %CLASSNAME%DataGridViewModel : IntegratedDataGridViewModelBase<Project, Data.Models.%CLASSNAME%>
{
    private readonly DataGridConfiguration<Data.Models.%CLASSNAME%> _defaultConfiguration =
        CreateDataGridConfiguration();

    public %CLASSNAME%DataGridViewModel(DataGridContext context, IProjectManager projectManager,
        IProjectStateService projectStateService,
        IFilterService filterService, IDispatcherService dispatcherService,
        IProjectDataGridService projectDataGridService, ITabService tabService,
        IAccentColorService accentColorService, IConfigurationService configurationService, IScopeManager scopeManager,
        IServiceLocator serviceLocator)
        : base(context, projectManager, projectStateService, filterService, dispatcherService, projectDataGridService,
            tabService, accentColorService,
            configurationService, scopeManager, serviceLocator)
    {
    }

    protected override Task InitializeAsync()
    {
        return base.InitializeAsync();
    }

    protected override Task CloseAsync()
    {
        return base.CloseAsync();
    }

    protected override void OnInteractionUpdated(IDataGridInteraction oldInteraction, IDataGridInteraction newInteraction)
    {
        base.OnInteractionUpdated(oldInteraction, newInteraction);
    }

    protected override IPropertyProvider GetPropertyProvider() => _defaultConfiguration.PropertyProvider;
    protected override ISettingsAdapter GetSettingsAdapter() => _defaultConfiguration.SettingsAdapter;

    protected override string GetTabScope()
    {
        return ScopeNames.%CLASSNAME%;
    }

    protected override async Task<IList<Data.Models.%CLASSNAME%>> GetProjectRecordsAsync(Project project)
    {
        return project?.MasterData.%CLASSNAME%s.ToList() ?? new List<Data.Models.%CLASSNAME%>(0);
    }

    protected override async void InitializeDataGrid(IDataGridInteraction interaction)
    {
        base.InitializeDataGrid(interaction);

        interaction.ApplyDefaultDataGridConfiguration(_defaultConfiguration);
    }

    private static DataGridConfiguration<Data.Models.%CLASSNAME%> CreateDataGridConfiguration()
    {
        var allocatedDataGridConfiguration = new DataGridConfiguration<Data.Models.%CLASSNAME%>()
            .SummaryRowLocation(RowGroupDetailsMode.Top)
            .StickyRowHeaders(true);

         // allocatedDataGridConfiguration.AddColumn(x => x.FieldName);
        %ADDCOLUMNS%

        return allocatedDataGridConfiguration;
    }

}

