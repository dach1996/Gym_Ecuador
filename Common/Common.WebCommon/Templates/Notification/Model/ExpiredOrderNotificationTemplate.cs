using Newtonsoft.Json;

namespace Common.WebCommon.Templates.Notification.Model;
/// <summary>
/// Modelo para expiración de orden
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="orderId"></param>
public class ExpiredOrderNotificationTemplate(string orderId) : NotificationTemplateModelBase
{
    /// <summary>
    /// Nombre de modelo
    /// </summary>
    public override NotificationTemplateName NotificationTemplateName => NotificationTemplateName.ExpiredOrder;

    /// <summary>
    /// Id de Órden
    /// </summary>
    /// <value></value>
    [JsonProperty(nameof(OrderId))]
    public string OrderId { get; set; } = orderId;
}