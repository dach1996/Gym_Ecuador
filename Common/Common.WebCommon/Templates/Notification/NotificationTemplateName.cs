
using System.Runtime.Serialization;
namespace Common.WebCommon.Templates.Notification;
/// <summary>
/// Notificaciónes template
/// </summary>
public enum NotificationTemplateName
{
    /// <summary>
    /// Expiración de Asiento
    /// </summary>
    [EnumMember(Value = nameof(ExpiredSeat))]
    ExpiredSeat,

    /// <summary>
    /// Expiración de Orden
    /// </summary>
    [EnumMember(Value = nameof(ExpiredOrder))]
    ExpiredOrder
}