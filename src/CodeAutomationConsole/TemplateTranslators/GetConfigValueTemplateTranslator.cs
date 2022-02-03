using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CodeAutomationConsole;

public class GetConfigValueTemplateTranslator : ITemplateTranslator
{
    private readonly SettingsValueResolver _settingsValueResolver = new SettingsValueResolver();

    public IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
    {
        var propertyPath = translationContext.Text;
        var context = translationContext.Context;
        var rootContext = translationContext.RootContext;

        if (context is null)
        {
            return new List<SettingValue>();
        }

        var result = _settingsValueResolver.TryGetValues(context, propertyPath)
                     ?? _settingsValueResolver.TryGetValues(rootContext, propertyPath);

        return result;
    }
}