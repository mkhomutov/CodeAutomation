using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeAutomationConsole;

public class GuidTemplateTranslator : TemplateTranslatorBase
{
    private readonly Dictionary<string, string> _guids = new Dictionary<string, string>();
    public override IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
    {
        var argument = translationContext.Argument;
        if (!_guids.TryGetValue(argument, out var guid))
        {
            guid = Guid.NewGuid().ToString().ToUpper();
            _guids[argument] = guid;
        }

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = translationContext.Context,
                Value = guid
            }
        };
    }
}

public class AssemblyNameTemplateTranslator : TemplateTranslatorBase
{
    public override IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
    {
        var values = SettingsValueTemplateTranslator.Translate(translationContext);
        var value = (string)values.FirstOrDefault()?.Value;

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = translationContext.Context,
                Value = value
            }
        };
    }
}

public class NamespaceTemplateTranslator : TemplateTranslatorBase
{
    public override IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
    {
        var values = SettingsValueTemplateTranslator.Translate(translationContext);
        var value = (string)values.FirstOrDefault()?.Value;

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = translationContext.Context,
                Value = value
            }
        };
    }
}

public class MapPropertiesTemplateTranslator : TemplateTranslatorBase
{
    public override IReadOnlyCollection<SettingValue> Translate(TranslationContext translationContext)
    {
        var values = SettingsValueTemplateTranslator.Translate(translationContext);
        var mapLines = values.Select(x => $"Map(x => x.{x.Value}).Name(\"{x.Value}\").AsString();");

        return new List<SettingValue>
        {
            new SettingValue
            {
                Context = translationContext.Context,
                Value = string.Join(Environment.NewLine, mapLines)
            }
        };
    }
}