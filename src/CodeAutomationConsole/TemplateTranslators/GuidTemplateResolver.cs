using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeAutomationConsole;

public class GuidTemplateResolver : TemplateResolverBase
{
    private readonly Dictionary<string, string> _guids = new Dictionary<string, string>();

    public override IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters)
    {
        var parameter = parameters[0];

        if (!_guids.TryGetValue(parameter, out var guid))
        {
            guid = Guid.NewGuid().ToString().ToUpper();
            _guids[parameter] = guid;
        }

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = target,
                Value = guid
            }
        };
    }
}