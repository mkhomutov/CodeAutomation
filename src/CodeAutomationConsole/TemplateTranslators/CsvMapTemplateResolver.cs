using System.Collections.Generic;
using System.Linq;

namespace CodeAutomationConsole;

public class CsvMapTemplateResolver : TemplateResolverBase
{
    public override IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters)
    {
        var parameter = parameters[1];
        switch (parameter)
        {
            case "className":
                return GetClassName(target);

            case "ctor":
                return GetCtor(target);
        }

        //var mapLines = values.Select(x => $"Map(x => x.{x.Value}).Name(\"{x.Value}\").AsString();");

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = target,
                Value = target
            }
        };
    }

    private IReadOnlyCollection<SettingValue> GetCtor(object target)
    {
        var result = ValueResolver.Resolve(target, null, "fields.name");
        return result.Select(x => new SettingValue
        {
            Context = x.Context,
            Value = $"Map(x => x.{x.Value}).Name(\"{x.Value}\").AsString();"
        }).ToArray();
    }

    private IReadOnlyCollection<SettingValue> GetClassName(object target)
    {
        var result = ValueResolver.Resolve(target, null, "className");
        return result.Select(x => new SettingValue
        {
            Context = x.Context,
            Value = $"{x.Value}Map"
        }).ToArray();
    }
}