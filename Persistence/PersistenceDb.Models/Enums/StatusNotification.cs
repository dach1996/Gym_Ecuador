namespace PersistenceDb.Models.Enums;

/// <summary>
/// Estado de Notificación Push
/// </summary>
public enum StatusNotification : byte
{
    /// <summary>
    /// Indefinido
    /// </summary>
    Initial = 1,

    /// <summary>
    /// Notificación pendiente
    /// </summary>
    Sending = 2,

    /// <summary>
    /// Notificación Enviada
    /// </summary>
    Sended = 3,

    /// <summary>
    /// Notificación con error
    /// </summary>
    Error = 4,
}