namespace PersistenceDb.Models.Enums;
/// <summary>
/// Tipo de notificación Push
/// </summary>
public enum PushNotificationType : byte
{
    /// <summary>
    /// Notificaciones personalizadas
    /// </summary>
    Token = 1,

    /// <summary>
    /// Notificaciones tipo tópico va a dirigido a segmentos
    /// </summary>
    Topic = 2
}