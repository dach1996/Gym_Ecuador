using Common.Templates.Configurations.Types;
using Common.Templates.Interface.Types;
using Common.Templates.Models.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Templates.Implementations.Types;
/// <summary>
/// Clase para Notificationes
/// </summary>
public class NotificationTemplate : TemplateBase, INotificationTemplate
{
    protected override string Section => "Templates:Notification";
    protected readonly IList<NotificationConfiguration> NotificationConfigurations;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    public NotificationTemplate(
        ILogger<NotificationTemplate> logger,
        IConfiguration configuration)
        : base(logger, configuration)
    {
        NotificationConfigurations = ConfigurationSection.Get<List<NotificationConfiguration>>();
    }

    /// <summary>
    /// Obtiene template con el Body
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="body"></param>
    /// <typeparam name="TBody"></typeparam>
    /// <returns></returns>
    public NotificationTemplateResponse GetTemplate<TBody>(string identifier, TBody body)
    {
        //Obtiene el Identificador
        var template = NotificationConfigurations.FirstOrDefault(first => first.Identifier == identifier)
            ?? throw new ArgumentException($"No se encuentra el template de Notificación con Identificador: {identifier}", nameof(identifier));
        //Remplaza los resultados
        var replaceBodyResult = ReplaceAttributes(template.Body, body);
        //Retorna respuesta
        return new(template.Title, replaceBodyResult);
    }

    /// <summary>
    /// Obtiene el template por el identificador
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public NotificationTemplateResponse GetTemplate(string identifier)
    {
        //Obtiene el Identificador
        var template = NotificationConfigurations.FirstOrDefault(first => first.Identifier == identifier)
            ?? throw new ArgumentException($"No se encuentra el template de Notificación con Identificador: {identifier}", nameof(identifier));
        //Retorna respuesta
        return new(template.Title, template.Body);
    }
}