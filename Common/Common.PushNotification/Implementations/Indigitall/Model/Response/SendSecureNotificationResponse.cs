namespace Common.PushNotification.Implementations.Indigitall.Model.Response;
/// <summary>
/// Respuesta de envío de Notificación por Campaña
/// </summary>
public class SendSecureNotificationResponse : IndigitallGenericResponse<List<SendSecureNotificationData>>
{

}

/// <summary>
/// Resultados Enviados
/// </summary>
public class SendSecureNotificationData
{
    /// <summary>
    /// Id de Notificación
    /// </summary>
    /// <value></value>
    public long NotificationId { get; set; }

    /// <summary>
    /// Id de Dispositivo
    /// </summary>
    /// <value></value>
    public string DeviceId { get; set; }

    /// <summary>
    /// Id externo
    /// </summary>
    /// <value></value>
    public string ExternalId { get; set; }

    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public string Status { get; set; }

    /// <summary>
    /// Error
    /// </summary>
    /// <value></value>
    public string Error { get; set; }
}

