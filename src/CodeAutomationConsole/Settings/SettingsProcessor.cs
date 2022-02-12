using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeAutomationConsole;

public class SettingsProcessor : ISettingsProcessor
{
    public string Name => throw new System.NotImplementedException();

    public AutomationSettings Run(AutomationSettings settings)
    {
        // TODO: implement csv files analyzing, merging settings and prepare settings for generating the code

        var config = GetConfig(settings);
        var project = GetProject(config);
        var csvList = GetCsvList(project);
        var existsCsv = GetExistsCsv(csvList);

        // Csv processing

        foreach (var file in Directory.GetFiles(config["csvPath"].ToString(),"*.csv"))
        {
            var csv = new CsvListMember(file);

            if (existsCsv.Contains(csv.File))
            {
                continue;
            }

            csv.Fields = new ParseCSV(file).Details;

            csvList.Add(csv.ToObject());
        }

        project["csvList"] = csvList;

        // Views processing

        var views = GetViews(project);

        var mainView = GetViewByName(views, "mainView");

        if (mainView.Count == 1)
        {
            var tabs = new List<object>();

            csvList.ForEach(csvObj =>
            {
                var csv = csvObj.ToObjectDictionary();

                var tab = new ViewTab(csv["className"].ToString(), csv["file"].ToString());
                var content = new Content();

                var dataGrid = new DataGrid((List<object>)csv["fields"]);

                content.DataGrid = dataGrid;
                tab.Content.Add(content);

                tabs.Add(tab.ToObject());
            });

            mainView["tabs"] = tabs;
            views.Add(mainView);
        }

        project["views"] = views;

        config["project"] = project;
        settings.Config = config;

        return settings;
    }

    private Dictionary<object, object> GetConfig(AutomationSettings settings)
    {
        var config = settings.Config is null ? new Dictionary<object, object>() : (Dictionary<object, object>)settings.Config;

        if (!config.ContainsKey("csvPath"))
        {
            config.Add("csvPath", "c:\\");
        }

        return config;
    }

    private Dictionary<object, object> GetProject(Dictionary<object, object> config)
    {
        if (config.ContainsKey("project"))
        {
            return (Dictionary<object, object>)config["project"];
        }
        else {
            return new Dictionary<object, object> { { "name", "ExampleProject" } };
        }
    }

    private List<object> GetCsvList(Dictionary<object, object> project)
    {
        return project.ContainsKey("csvList") ? (List<object>)project["csvList"] : new List<object>();
    }

    private IEnumerable<string> GetExistsCsv(List<object> csvList)
    {
        return csvList.Select(csv =>
        {
            var csvObject = (Dictionary<object, object>)(IEnumerable<KeyValuePair<object, object>>)csv;
            return csvObject.ContainsKey("file") ? csvObject["file"].ToString() : null;
        });
    }

    private List<object> GetViews(Dictionary<object, object> project)
    {
        if (project.ContainsKey("views"))
        {
            return (List<object>)project["views"];
        }
        else
        {
            return new List<object>();
        }

    }

    private Dictionary<object, object> GetViewByName(List<object> views, string name)
    {
        var namedView = views.Find(v =>
        {
            var view = (Dictionary<object, object>)v;
            if (view.ContainsKey("name"))
            {
                return name.Equals(view["name"].ToString());
            }
            else
            {
                return false;
            }
        });

        if (namedView is null)
        {
            return new Dictionary<object, object> { { "name", name } };
        }
        else
        {
            return (Dictionary<object, object>)namedView;
        }
    }

}