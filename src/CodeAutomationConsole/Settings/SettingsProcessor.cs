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
        var csvList = GetKey(project, "csvList");
        var existsCsv = GetExistsCsv(csvList);

        // Csv processing

        if (settings.DataSource.DataSourceType.Equals("csv"))
        {
            var csvPath = settings.DataSource.ConnectionString;

            foreach (var file in Directory.GetFiles(csvPath,"*.csv"))
            {
                if (existsCsv.Contains(Path.GetFileNameWithoutExtension(file)))
                {
                    continue;
                }

                var csv = new CsvListMember(file);

                csvList.Add(csv.ToObject());
            }
        }

        var csvListExportFile = Path.Combine(settings.OutputPath, "csvList.yml");
        csvList.ExportYaml(csvListExportFile);

        //Uncomment for using ScriptObject instead this object
        //project["csvList"] = new Dictionary<string, string>() { { "import ", csvListExportFile } };
        project["csvList"] = csvList;       // remove after implement using ScriptObject instead this object

        // Views processing

        var views = GetKey(project, "views");

        var mainView = SearchByKeyValue(views, "name", "mainView");

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

        var viewsExportFile = Path.Combine(settings.OutputPath, "views.yml");
        views.ExportYaml(viewsExportFile);

        //Uncomment for using ScriptObject instead this object
        //project["views"] = new Dictionary<string, string>() { { "import ", viewsExportFile } };
        project["views"] = views;       // remove after implement using ScriptObject instead this object

        config["project"] = project;
        settings.Config = config;

        return settings;
    }

    private Dictionary<object, object> GetConfig(AutomationSettings settings)
    {
        return settings.Config is null ? new Dictionary<object, object>() : (Dictionary<object, object>)settings.Config;
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

    private List<object> GetKey(Dictionary<object, object> @object, string key)
    {
        if (@object.ContainsKey(key))
        {
            if (@object[key].GetType() == typeof(List<object>))
            {
                return (List<object>) @object[key];
            }

            if (@object[key].GetType() == typeof(Dictionary<object, object>))
            {
                var keyObj = (Dictionary<object, object>)@object[key];
                if (keyObj.ContainsKey(key))
                {
                    return (List<object>)keyObj["import"].ToString().ImportFromYaml();
                }
            }
        }

        return new List<object>();
    }

    private IEnumerable<string> GetExistsCsv(List<object> csvList)
    {
        return csvList.Select(csv =>
        {
            var csvObject = (Dictionary<object, object>)(IEnumerable<KeyValuePair<object, object>>)csv;
            return csvObject.ContainsKey("file") ? csvObject["file"].ToString() : null;
        });
    }

    private Dictionary<object, object> SearchByKeyValue(List<object> obj, string key, string value)
    {
        var result = obj.Find(v =>
        {
            var view = (Dictionary<object, object>)v;
            if (view.ContainsKey(key))
            {
                return value.Equals(view[key].ToString());
            }
            else
            {
                return false;
            }
        });

        if (result is null)
        {
            return new Dictionary<object, object> { { key, value } };
        }
        else
        {
            return (Dictionary<object, object>)result;
        }
    }

}