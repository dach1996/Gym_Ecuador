namespace LogicApi.Model.Response.Order;
/// <summary>
/// Respuesta del servicio Generar Orden
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="orderGuid"></param>
/// <param name="userMessage"></param>
public class GenerateOrderResponse(Guid orderGuid, string userMessage) : IApiBaseResponse
{
    /// <summary>
    /// Número de Orden
    /// </summary>
    /// <value></value>
    public Guid OrderGuid { get; set; } = orderGuid;

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; } = userMessage;

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; } = true;
}