using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CodeAutomationConsole;

public class ValueResolver : ITemplateTranslator
{
    private readonly SettingsValueResolver _settingsValueResolver = new SettingsValueResolver();

    public IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
    {
        return Resolve(translationContext.Context, translationContext.RootContext, translationContext.Argument);
    }

    public IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters)
    {
        if (target is null)
        {
            return new List<SettingValue>();
        }

        var propertyPath = parameters[0];

        var result = _settingsValueResolver.TryGetValues(target, propertyPath);
        if (result is null || !result.Any())
        {
            result = _settingsValueResolver.TryGetValues(fallbackTarget, propertyPath);
        }

        return result;
    }
}