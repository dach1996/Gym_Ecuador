namespace Common.PushNotification.Model;
/// <summary>
/// Respuesta para notificaciones
/// </summary>
public class NotificationResponse
{
    /// <summary>
    /// Respuestas
    /// </summary>
    /// <value></value>
    public IEnumerable<NotificationItem> NotificationItems { get; private set; }

    /// <summary>
    /// Notificaciones Completas
    /// </summary>
    /// <value></value>
    public int SuccessCount { get => NotificationItems.Count(t => t.IsSuccess); }

    /// <summary>
    /// Notificaciones con Error
    /// </summary>
    /// <value></value>
    public int FailureCount { get => NotificationItems.Count(t => !t.IsSuccess); }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="sendNotificationResponses"></param>
    public NotificationResponse(IEnumerable<NotificationItem> sendNotificationResponses)
        => NotificationItems = sendNotificationResponses;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="topic"></param>
    public NotificationResponse(string messageId, string topic, bool success = true)
        => NotificationItems = [success? NotificationItem.Success(messageId, topic) :NotificationItem.Fail(messageId, topic)];
}

/// <summary>
/// Respuesta de envío
/// </summary>
public class NotificationItem
{
    /// <summary>
    /// Id de Mensaje
    /// </summary>
    /// <value></value>
    public string Identifier { get; set; }

    /// <summary>
    /// Id de Mensaje
    /// </summary>
    /// <value></value>
    public string MessageId { get; set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public string Message { get; set; }

    /// <summary>
    /// Verificación
    /// </summary>
    /// <value></value>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="message"></param>
    /// <param name="isSuccess"></param>
    private NotificationItem(string messageId, string message = null, bool isSuccess = true, string identifier = null)
    {
        MessageId = messageId;
        Message = message;
        IsSuccess = isSuccess;
        Identifier = identifier;
    }

    /// <summary>
    /// Crea una instancia Correcta
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="identifier"></param>
    /// <returns></returns>
    public static NotificationItem Success(string messageId, string identifier)
        => new(messageId, isSuccess: true, identifier: identifier);

    /// <summary>
    /// Crea una instancia con error
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="identifier"></param>
    /// <param name="messageError"></param>
    /// <returns></returns>
    public static NotificationItem Fail(string identifier, string messageError)
        => new(null, messageError, isSuccess: false, identifier: identifier);
}