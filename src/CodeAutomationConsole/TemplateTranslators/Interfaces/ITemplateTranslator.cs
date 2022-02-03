using System.Collections;
using System.Collections.Generic;

namespace CodeAutomationConsole;

public interface ITemplateTranslator
{
    IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext);
}