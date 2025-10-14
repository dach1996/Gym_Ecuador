using LogicApi.Model.Enum;

namespace LogicApi.Model.Response.Order.Common;
/// <summary>
/// Item de Órden
/// </summary>
public class GenerateOrderItem
{
    /// <summary>
    /// Lugar de Origen
    /// </summary>
    /// <value></value>
    public string OriginProvinceName { get; set; }

    /// <summary>
    /// Estación de Origen
    /// </summary>
    /// <value></value>
    public string OriginTransportPointName { get; set; }

    /// <summary>
    /// Lugar de Destino
    /// </summary>
    /// <value></value>
    public string DestinationProvinceName { get; set; }

    /// <summary>
    /// Estación de Destino
    /// </summary>
    /// <value></value>
    public string DestinationTransportPointName { get; set; }

    /// <summary>
    /// Fecha de Salida
    /// </summary>
    /// <value></value>
    public DateTime DateTimeOrigin { get; set; }

    /// <summary>
    /// Fecha de Llegada
    /// </summary>
    /// <value></value>
    public DateTime DateTimeDestination { get; set; }

    /// <summary>
    /// Nombre de compania
    /// </summary>
    /// <value></value>
    public string CompanyName { get; set; }

    /// <summary>
    /// Información de asientos con personas
    /// </summary> 
    /// <value></value>
    public IEnumerable<SeatPersonInformation> SeatPersonInformation { get; set; }
}

/// <summary>
/// Item de orden generada con información adicional 
/// </summary>
public class GenerateOrderItemAdditionalInformation : GenerateOrderItem
{
    /// <summary>
    /// Id de orden
    /// </summary>
    /// <value></value>
    public Guid OrderGuid { get; set; }

    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public OrderViewState State { get; set; }
}

/// <summary>
/// Información de persona y asiento
/// </summary>
public record SeatPersonInformation
{
    /// <summary>
    /// Identificador de ASiento
    /// </summary>
    /// <value></value>
    public string SeatIdentifier { get; set; }

    /// <summary>
    /// Tipo de Asiento
    /// </summary>
    /// <value></value>
    public string SeatType { get; set; }

    /// <summary>
    /// Nombre de Persona
    /// </summary>
    /// <value></value>
    public string PersonName { get; set; }

    /// <summary>
    /// Identificacióin de Persona
    /// </summary> <summary>
    /// 
    /// </summary>
    /// <value></value>
    public string PersonIdentifier { get; set; }
}