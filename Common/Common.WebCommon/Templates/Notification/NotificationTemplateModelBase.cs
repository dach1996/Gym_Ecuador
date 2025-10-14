namespace Common.WebCommon.Templates.Notification;
/// <summary>
/// Base de modelo para Template de notificación
/// </summary>
public abstract class NotificationTemplateModelBase
{
    /// <summary>
    /// Nombre de Template de Notificación
    /// </summary>
    /// <value></value>
    public abstract NotificationTemplateName NotificationTemplateName { get; }
}