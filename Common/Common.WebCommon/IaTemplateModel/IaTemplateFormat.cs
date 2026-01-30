using System.Reflection;
using Common.WebCommon.Models;
using Newtonsoft.Json;

namespace Common.WebCommon.IaTemplateModel;

public class IaTemplateFormat(AppSettingsCommon appSettingsApi)
{
    /// <summary>
    /// Obtiene el modelo de Template 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public IaTemplateResponse GetTemplate(IIaTemplateModel model)
    {
        var template = appSettingsApi.IATemplates.GetValueOrDefault($"{model.TemplateName}")
            ?? throw new ArgumentException($"No se pudo encontrar el Identificador: '{model.TemplateName}' en los Templates configurados");
        var behavior = template.Behavior ?? string.Empty;
        var listParams = model.GetType()
                .GetProperties()
                .Select(prop => new KeyValuePair<string, string>
                    ($"{{{{{prop.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? prop.Name}}}}}", $"{prop.GetValue(model, null)}"))
                .ToDictionary(x => x.Key, x => x.Value);
        foreach (var parameter in listParams)
            behavior = behavior.Replace(parameter.Key, parameter.Value);

        var indications = template.Indications ?? string.Empty;
        foreach (var parameter in listParams)
            indications = indications.Replace(parameter.Key, parameter.Value);

        return new IaTemplateResponse
        {
            Behavior = behavior,
            Indications = indications,
            AiImplementation = template.AiImplementation,
            ResponseType = template.ResponseType
        };
    }
}