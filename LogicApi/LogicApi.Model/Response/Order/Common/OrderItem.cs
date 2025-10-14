using LogicApi.Model.Enum;

namespace LogicApi.Model.Response.Order.Common;
/// <summary>
/// Información de orden
/// </summary>
public class OrderItem
{
    /// <summary>
    /// Id de Origen
    /// </summary>
    /// <value></value>
    public Guid OrderGuid { get; set; }

    /// <summary>
    /// Fecha de Salida
    /// </summary>
    /// <value></value>
    public DateTime OriginDateTime { get; set; }

    /// <summary>
    /// Fecha de Llegada
    /// </summary>
    /// <value></value>
    public DateTime DestinationDateTime { get; set; }

    /// <summary>
    /// Origen Provincia
    /// </summary>
    /// <value></value>
    public string OriginProvinceName { get; set; }

    /// <summary>
    /// Destino Provincia
    /// </summary>
    /// <value></value>
    public string DestinationProvinceName { get; set; }

    /// <summary>
    /// Origen Estación
    /// </summary>
    /// <value></value>
    public string OriginTransportPointName { get; set; }

    /// <summary>
    /// Destino Estación
    /// </summary>
    /// <value></value>
    public string DestinationTransportPointName { get; set; }

    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public OrderViewState State { get; set; }
}