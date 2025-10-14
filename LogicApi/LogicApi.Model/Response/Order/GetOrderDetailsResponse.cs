using LogicApi.Model.Response.Order.Common;

namespace LogicApi.Model.Response.Order;
/// <summary>
/// Respuesta  de obtener detalle de la orden
/// </summary>
public class GetOrderDetailsResponse(GenerateOrderItemAdditionalInformation orderItem, IDictionary<string, decimal> values) : IApiBaseResponse
{
    /// <summary>
    /// Item Respuesta
    /// </summary>
    /// <value></value>
    public GenerateOrderItemAdditionalInformation OrderItem { get; set; } = orderItem;

    /// <summary>
    /// Valores a desglosar
    /// </summary>
    /// <value></value>
    public IDictionary<string, decimal> Values { get; set; } = values;

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }


}
