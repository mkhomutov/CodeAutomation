using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeAutomationConsole;

public class MasterDataTemplateResolver : TemplateResolverBase
{
    public override IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters)
    {
        var values = ValueResolver.Resolve(target, fallbackTarget, parameters);
        var parameter = parameters[1];
        switch (parameter)
        {
            case "properties":
                return GetProperties(target);

            case "ctor":
                return GetCtor(target);

            case "loadRecords":
                return GetLoadRecordsCode(target);
        }

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = target,
                Value = target
            }
        };
    }

    private IReadOnlyCollection<SettingValue> GetLoadRecordsCode(object target)
    {
        var fileNames = ValueResolver.Resolve(target, null, "csvList.className")
            .Select(x => $"var {x.Value}File = Path.Combine(location, FileNames.{GetFileName(x.Context)});");

        var loadRecords = ValueResolver.Resolve(target, null, "csvList.className")
            .Select(x => $"project.MasterData.{GetPropertyName(x.Context)} = _projectSerializationService.LoadRecords<{x.Value}, {x.Value}Map>({x.Value}File);");

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = target,
                Value = string.Join(Environment.NewLine, fileNames) + Environment.NewLine + string.Join(Environment.NewLine, loadRecords)
            }
        };
    }

    private IReadOnlyCollection<SettingValue> GetCtor(object target)
    {
        var ctorLines = ValueResolver.Resolve(target, null, "csvList.className")
            .Select(x => $"{GetPropertyName(x.Context) ?? x.Value} = new List<{x.Value}>();");

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = target,
                Value = string.Join(Environment.NewLine, ctorLines)
            }
        };
    }

    private IReadOnlyCollection<SettingValue> GetProperties(object target)
    {
        var properties = ValueResolver.Resolve(target, null, "csvList.className")
            .Select(x => $"public IReadOnlyCollection<{x.Value}> {GetPropertyName(x.Context) ?? x.Value} {{ get; set; }}");

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = target,
                Value = string.Join(Environment.NewLine, properties)
            }
        };
    }

    private string GetPropertyName(object context)
    {
        return (string)ValueResolver.Resolve(context, null, "file").FirstOrDefault()?.Value;
    }

    private string GetFileName(object context)
    {
        return (string)ValueResolver.Resolve(context, null, "file").FirstOrDefault()?.Value;
    }
}