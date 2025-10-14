using Common.WebCommon.Models.Enum;

namespace LogicApi.Model.Response.Ticket;

/// <summary>
/// Obtiene la lista de Tickets disponibles
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="ticketsAvailable"></param>
public class GetTicketsAvailableResponse(
   IEnumerable<TicketAvailable> ticketsAvailable) : IApiBaseResponse
{
    /// <summary>
    /// Pasajes Diponibles
    /// </summary>
    /// <value></value>
    public IEnumerable<TicketAvailable> TicketsAvailable { get; set; } = ticketsAvailable;

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

/// <summary>
/// Ticket Disponible
/// </summary>
public class TicketAvailable
{
    /// <summary>
    /// Identificador
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public string Identifier { get; set; }

    /// <summary>
    /// Guid de la Ruta
    /// </summary>
    /// <value></value>
    public Guid RouteGuid { get; set; }

    /// <summary>
    /// Compañia
    /// </summary>
    /// <value></value>
    public CooperativeInformation Cooperative { get; set; }

    /// <summary>
    /// Asientos Disponibles
    /// </summary>
    /// <value></value>
    public int SeatAvailable { get; set; }

    /// <summary>
    /// Horario
    /// </summary>
    /// <value></value>
    public Schedule Schedule { get; set; }

    /// <summary>
    /// Tipo de Bus
    /// </summary>
    /// <value></value>
    public BusType BusType { get; set; }

    /// <summary>
    /// Id de Cooperativa de Bus
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public int BusCooperativeId { get; set; }

    /// <summary>
    /// Precio Aproximado
    /// </summary>
    /// <value></value>
    public decimal ApproximatePrice { get; set; }

    /// <summary>
    /// Mejor Opción
    /// </summary>
    /// <value></value>
    public bool IsBest { get; set; }

    /// <summary>
    /// Código de Punto de Origen
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public int OriginTransportPointId { get; set; }

    /// <summary>
    /// Punto de Destino
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public int DestinationTransportPointId { get; set; }
}

/// <summary>
/// Información de Cooperativa
/// </summary>
public class CooperativeInformation
{
    /// <summary>
    /// Nombre de compañia de bus
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Imagen de Compañia de Bus
    /// </summary>
    /// <value></value>
    public string UrlImage { get; set; }

    /// <summary>
    /// Id de Cooperativa
    /// </summary>
    /// <value></value>
    [JsonIgnore]
    public int CooperativeId { get; set; }
}


/// <summary>
/// Horario
/// </summary>
public class Schedule
{
    /// <summary>
    /// Nombre de Punto de Origen
    /// </summary>
    /// <value></value>
    public string OriginTransportPointName { get; set; }

    /// <summary>
    /// Nombre de Punto de Destino
    /// </summary>
    /// <value></value>
    public string DestinationTransportPointName { get; set; }


    /// <summary>
    /// Nombre de Provincia de Origen
    /// </summary>
    /// <value></value>
    public string ProvinceOriginName { get; set; }

    /// <summary>
    /// Nombre de Provincia de Destino
    /// </summary>
    /// <value></value>
    public string ProvinceDestinationName { get; set; }

    /// <summary>
    /// Fecha de Salida
    /// </summary>
    /// <value></value>
    public DateTime OriginDateTime { get; set; }

    /// <summary>
    /// Fecha de llegada
    /// </summary>
    /// <value></value>
    public DateTime DestinationDateTime { get; set; }
}

/// <summary>
/// Información Parcial de Ticket
/// </summary>

public class TicketPartialInformation
{
    /// <summary>   
    /// Nombre de Provincia de Origen
    /// </summary>
    /// <value></value>
    public string OriginProvinceName { get; set; }

    /// <summary>
    /// Nombre de Provincia de Destino
    /// </summary>
    /// <value></value>
    public string DestinationProvinceName { get; set; }
}
