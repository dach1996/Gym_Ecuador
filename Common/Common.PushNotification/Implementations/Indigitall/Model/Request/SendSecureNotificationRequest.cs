
namespace Common.PushNotification.Implementations.Indigitall.Model.Request;
/// <summary>
/// Request para Enviar notificación
/// </summary>
public class SendSecureNotificationRequest
{
    /// <summary>
    /// Tipo
    /// </summary>
    /// <value></value>
    public string IdType { get; set; }

    /// <summary>
    /// Es disipable
    /// </summary>
    /// <value></value>
    public bool IsDisposable { get; set; }

    /// <summary>
    /// Mensaje silencioso
    /// </summary>
    /// <value></value>
    public bool Silent { get; set; }

    /// <summary>
    /// Dipositivo
    /// </summary>
    /// <value></value>
    public string Device { get; set; }

    /// <summary>
    /// Id de Aplicación
    /// </summary> <summary>
    /// <value></value>
    public long ApplicationId { get; set; }

    /// <summary>
    /// Información Adicional
    /// </summary>
    /// <value></value>
    public string Data { get; set; }

    /// <summary>
    /// Campos Personalizados
    /// </summary>
    /// <value></value>
    public CustomFields CustomFields { get; set; }
}

/// <summary>
/// Campos personalizados
/// </summary>
public class CustomFields
{
    /// <summary>
    /// Título
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public string Body { get; set; }
}