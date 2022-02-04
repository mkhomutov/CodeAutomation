using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CodeAutomationConsole;

public class SettingsValueTemplateTranslator : ITemplateTranslator
{
    private readonly SettingsValueResolver _settingsValueResolver = new SettingsValueResolver();

    public IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
    {
        var propertyPath = translationContext.Argument;
        var context = translationContext.Context;
        var rootContext = translationContext.RootContext;

        if (context is null)
        {
            return new List<SettingValue>();
        }

        var result = _settingsValueResolver.TryGetValues(context, propertyPath);
        if (result is null || !result.Any())
        {
            result = _settingsValueResolver.TryGetValues(rootContext, propertyPath);
        }

        return result;
    }
}