using LogicApi.Model.Response.Order.Common;
namespace LogicApi.Model.Response.Order;
/// <summary>
/// Respuesta del servicio Generar Orden
/// </summary>
public class GetInitialMyOrdersResponse(IEnumerable<OrderItemActive> ordersItemActive)
{
    /// <summary>
    /// Items de Ordenes Activos
    /// </summary>
    /// <value></value>
    public IEnumerable<OrderItemActive> OrdersItemActive { get; set; } = ordersItemActive;
}

/// <summary>
/// Información de orden
/// </summary>
public class OrderItemActive : OrderItem
{
    /// <summary>
    /// Fecha de expiración de la Orden
    /// </summary>
    /// <value></value>
    public DateTime DateTimeExpired { get; set; }
}