using System.Collections;
using System.Collections.Generic;

namespace CodeAutomationConsole;

public interface ITemplateResolver
{
    IReadOnlyCollection<SettingValue> Resolve(object target, object fallbackTarget, params string[] parameters);
}