using Common.Templates.Interface.Types;

namespace Common.Templates.Interface;
/// <summary>
/// Template Factory
/// </summary>
public interface ITemplateFactory
{
    /// <summary>
    /// Template de Notificación
    /// </summary>
    INotificationTemplate NotificationTemplate { get; }
    /// <summary>
    /// Template de Mail
    /// </summary>
    IMailTemplate MailTemplate { get; }
}