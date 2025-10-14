using Common.Cooperative.Models.Enums;

namespace Common.Cooperative.Models.Response;
/// <summary>
/// Respuesta para obtener los vuelos disponibles
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="cooperative"></param>
/// <param name="ticketAvailables"></param>
public class GetAvailableTripsResponse(CooperativeImplementationName cooperative, IEnumerable<Ticket> ticketAvailables)
{
    /// <summary>
    /// Código de Categoría
    /// </summary>
    /// <value></value>
    public CooperativeImplementationName Cooperative { get; set; } = cooperative;

    /// <summary>
    /// Tickets Disponibles
    /// </summary>
    /// <value></value>
    public IEnumerable<Ticket> TicketAvailables { get; set; } = ticketAvailables;
}


/// <summary>
/// Ticket Disponible
/// </summary>
public class Ticket
{
    /// <summary>
    /// Identificador de Ticket
    /// </summary>
    /// <value></value>
    public string Identifier { get; set; }

    /// <summary>
    /// Tipos de Bus
    /// </summary>
    /// <value></value>
    public BusType BusType { get; set; }

    /// <summary>
    /// Disponibles
    /// </summary>
    /// <value></value>
    public int SeatAvailable { get; set; }

    /// <summary>
    /// Horario
    /// </summary>
    /// <value></value>
    public Schedule Schedule { get; set; }

    /// <summary>
    /// Precio Aproximado
    /// </summary>
    /// <value></value>
    public decimal ApproximatePrice { get; set; }

    /// <summary>
    /// Código de Estación de Origen
    /// </summary>
    /// <value></value>
    public string OriginStationCode { get; set; }

    /// <summary>
    /// Código de Estación de Destino
    /// </summary>
    /// <value></value>
    public string DestinationStationCode { get; set; }

    /// <summary>
    /// Código de Bus
    /// </summary>
    /// <value></value>
    public string BusCode { get; set; }
}

/// <summary>
/// Horario
/// </summary>
public class Schedule
{

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