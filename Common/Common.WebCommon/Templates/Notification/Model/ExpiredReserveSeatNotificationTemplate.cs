namespace Common.WebCommon.Templates.Notification.Model;
/// <summary>
/// Modelo para expiración de asiento
/// </summary>
public class ExpiredReserveSeatNotificationTemplate : NotificationTemplateModelBase
{
    /// <summary>
    /// Nombre de modelo
    /// </summary>
    public override NotificationTemplateName NotificationTemplateName => NotificationTemplateName.ExpiredSeat;
}