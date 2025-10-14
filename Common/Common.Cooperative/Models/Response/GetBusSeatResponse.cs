using Common.Cooperative.Models.Enums;

namespace Common.Cooperative.Models.Response;
/// <summary>
/// Respuesta para obtener los vuelos disponibles
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="busIdentifier"></param>
/// <param name="seats"></param>
public class GetBusSeatResponse(string busIdentifier, Seat[] seats)
{

    /// <summary>
    /// Identificador de Bus
    /// </summary>
    /// <value></value>
    public string BusIdentifier { get; set; } = busIdentifier;

    /// <summary>
    /// Lista de Asientos
    /// </summary>
    /// <value></value>
    public Seat[] Seats { get; set; } = seats;
    
}

/// <summary>
/// Asientos
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="identifier"></param>
/// <param name="floorIdentifier"></param>
/// <param name="state"></param>
/// <param name="price"></param>
public class Seat(string identifier, string floorIdentifier = "", decimal? price = null, SeatState state = SeatState.Purchased)
{

    /// <summary>
    /// Identificador
    /// </summary>
    /// <value></value>
    public string SeatIdentifier { get; set; } = identifier;

    /// <summary>
    /// Identificador de Piso
    /// </summary>
    /// <value></value>
    public string FloorIdentifier { get; set; } = floorIdentifier;

    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public SeatState State { get; set; } = state;

    /// <summary>
    /// Precio
    /// </summary>
    /// <value></value>
    public decimal? Price { get; set; } = price;
}

