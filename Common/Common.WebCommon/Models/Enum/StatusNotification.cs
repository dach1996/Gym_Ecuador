using System.Runtime.Serialization;

namespace Common.WebCommon.Models.Enum;

/// <summary>
/// Estado de Notificación Push
/// </summary>
public enum StatusNotification
{
    /// <summary>
    /// Notificación pendiente
    /// </summary>
    [EnumMember(Value = "Pendiente")]
    Sending = 1,

    /// <summary>
    /// Notificación Enviada
    /// </summary>
    [EnumMember(Value = "Enviada")]
    Sended = 2,

    /// <summary>
    /// Notificación con error
    /// </summary>
    [EnumMember(Value = "Error")]
    Error = -2,

    /// <summary>
    /// Indefinido
    /// </summary>
    [EnumMember(Value = "Indefinido")]
    Initial = -1,
}