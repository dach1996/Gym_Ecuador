
namespace PersistenceDb.Models.Enums;
/// <summary>
/// Implementación de envío de notificaciones
/// </summary>
public enum NotificationPushImplementationType : byte
{
    /// <summary>
    ///  Descendente
    /// </summary>
    Firebase = 1,

    /// <summary>
    /// Ascendente
    /// </summary>
    Huawei = 2
}

