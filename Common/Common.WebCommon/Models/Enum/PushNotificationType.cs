using System.Runtime.Serialization;

namespace Common.WebCommon.Models.Enum;
/// <summary>
/// Tipo de notificación Push
/// </summary>
public enum PushNotificationType
{
    /// <summary>
    /// Notificaciones personalizadas
    /// </summary>
    [EnumMember(Value = "Token")]
    Custom = 1,

    /// <summary>
    /// Notificaciones tipo tópico va a dirigido a segmentos
    /// </summary>
    [EnumMember(Value = "Tópico")]
    Topic = 2
}