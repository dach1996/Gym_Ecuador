using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.Templates.Implementations.Types;
/// <summary>
/// Clase base para template
/// </summary>
public abstract class TemplateBase
{
    protected readonly IConfigurationSection ConfigurationSection;
    protected abstract string Section { get; }
    protected readonly ILogger<TemplateBase> Logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    protected TemplateBase(ILogger<TemplateBase> logger, IConfiguration configuration)
    {
        ConfigurationSection = configuration.GetSection(Section);
        if (!ConfigurationSection.Exists())
            throw new InvalidOperationException($"No se encuentra la sección de configuración de templates: {Section}");
        Logger = logger;
    }

    /// <summary>
    /// Remplaza los datos del template
    /// </summary>
    /// <param name="template"></param>
    /// <param name="model"></param>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    protected static string ReplaceAttributes<TModel>(string template, TModel model)
    {
        var message = template;
        var listParams = model.GetType()
                .GetProperties()
                .Where(prop => prop.GetCustomAttribute<JsonPropertyAttribute>() is not null)
                .Select(prop => new KeyValuePair<string, string>
                    ($"{{{{{prop.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName}}}}}", $"{prop.GetValue(model, null)}"))
                .ToDictionary(x => x.Key, x => x.Value);
        foreach (var parameter in listParams)
            message = message.Replace(parameter.Key, parameter.Value);
        return message;
    }
}