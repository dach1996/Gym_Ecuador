using LogicApi.Model.Response.Order.Common;

namespace LogicApi.Model.Response.Order;
/// <summary>
/// Respuesta del servicio Generar Orden
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetMyOrdersPaginatedResponse(int totalRegister, IEnumerable<OrderItem> registers) : IPaginatorApiResponse<OrderItem>
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Total de Registros
    /// </summary>
    /// <value></value>
    public int TotalRegister { get; set; } = totalRegister;

    /// <summary>
    /// Registros
    /// </summary>
    /// <value></value>
    public IEnumerable<OrderItem> Registers { get; set; } = registers;
}
