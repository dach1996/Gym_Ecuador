using LogicApi.Model.Response.Order.Common;

namespace LogicApi.Model.Response.Order;
/// <summary>
/// Respuesta del servicio Generar Orden
/// </summary>
public class VerifyOrderValuesResponse : IApiBaseResponse
{
    /// <summary>
    /// Lista de órdenes a calcular montos
    /// </summary>
    /// <value></value>
    public IEnumerable<GenerateOrderItem> Orders { get; set; }

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Valores a desglosar
    /// </summary>
    /// <value></value>
    public IDictionary<string, decimal> Values { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }
}

