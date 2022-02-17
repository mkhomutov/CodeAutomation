using System.Collections.Generic;

namespace CodeAutomationConsole
{

    public abstract class TemplateResolverBase : ITemplateResolver
    {
        protected static readonly ITemplateResolver ValueResolver =
            new ValueResolver();

        public abstract IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters);
    }
}
