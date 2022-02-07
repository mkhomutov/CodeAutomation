using System.Collections;
using System.Collections.Generic;

namespace CodeAutomationConsole;

public interface ITemplateTranslator : ITemplateResolver
{
    IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext);
}

public interface ITemplateResolver
{
    IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters);
}