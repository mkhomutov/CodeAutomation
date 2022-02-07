using System.Collections.Generic;

namespace CodeAutomationConsole
{

    public abstract class TemplateResolverBase : ITemplateTranslator
    {
        protected static readonly ITemplateResolver ValueResolver =
            new ValueResolver();

        public IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
        {
            return Resolve(translationContext.Context, translationContext.RootContext, translationContext.Argument);
        }

        public abstract IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters);
    }
}
