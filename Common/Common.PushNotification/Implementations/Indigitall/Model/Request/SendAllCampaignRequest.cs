using Common.PushNotification.Implementations.Indigitall.Model.Enum;

namespace Common.PushNotification.Implementations.Indigitall.Model.Request;
/// <summary>
/// Request para Enviar notificación
/// </summary>
public class SendAllCampaignRequest
{
    /// <summary>
    /// Ide Aplicación
    /// </summary>
    /// <value></value>
    public long ApllicationId { get; set; }

    /// <summary>
    /// Información
    /// </summary>
    /// <value></value>
    public string Data { get; set; }

    /// <summary>
    /// Data Segura
    /// </summary>
    /// <value></value>
    public string SecuredData { get; set; }

    /// <summary>
    /// Tipo de Envío
    /// </summary>
    /// <value></value>
    public IdType IdType { get; set; }

    /// <summary>
    /// Dispositivo
    /// </summary>
    /// <value></value>
    public string Device { get; set; }

    /// <summary>
    /// Información adicional
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> CustomFields { get; set; }

    /// <summary>
    /// Infica si la información puede ser disipada
    /// </summary>
    /// <value></value>
    public bool IsDisposable { get; set; }

    /// <summary>
    /// Indica si es un envío silencioso
    /// </summary>
    /// <value></value>
    public bool Silent { get; set; }
}
