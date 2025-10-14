using Common.Templates.Models.Types;

namespace Common.Templates.Interface.Types;
/// <summary>
/// Templates de Notificación
/// </summary>
public interface INotificationTemplate
{
    /// <summary>
    /// Obtiene template con el Body 
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="body"></param>
    /// <typeparam name="TBody"></typeparam>
    /// <returns></returns>
    NotificationTemplateResponse GetTemplate<TBody>(string identifier, TBody body);

    /// <summary>
    /// Obtiene template con el Body 
    /// </summary>
    /// <param name="identifier"></param>
    /// <returns></returns>
    NotificationTemplateResponse GetTemplate(string identifier);
}