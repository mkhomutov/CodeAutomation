using System.Collections.Generic;

namespace CodeAutomationConsole
{

    public abstract class TemplateTranslatorBase : ITemplateTranslator
    {
        protected static readonly ITemplateTranslator SettingsValueTemplateTranslator =
            new SettingsValueTemplateTranslator();
        public abstract IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext);
    }
}
