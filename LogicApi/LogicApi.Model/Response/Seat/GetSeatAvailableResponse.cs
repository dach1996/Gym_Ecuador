using LogicApi.Model.Common;
using LogicApi.Model.Enum;

namespace LogicApi.Model.Response.Seat;
/// <summary>
/// Asientos Disponibles
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="routeGuid"></param>
/// <param name="services"></param>
/// <param name="floors"></param>
/// <param name="seats"></param>
public class GetSeatAvailableResponse(
    Guid routeGuid,
    IEnumerable<string> services,
    IEnumerable<FloorInformation> floors,
    IEnumerable<SeatNewStateModel> seats)
{

    
    /// <summary>
    /// Guid de la Ruta
    /// </summary>
    /// <value></value>
    public Guid RouteGuid { get; set; } = routeGuid;

    /// <summary>
    /// Servicios
    /// </summary>
    /// <value></value>
    public IEnumerable<string> Services { get; set; } = services;

    /// <summary>
    /// Espacios
    /// </summary>
    /// <value></value>
    public IEnumerable<FloorInformation> Floors { get; set; } = floors;

    /// <summary>
    /// Asientos comprados
    /// </summary>
    /// <value></value>
    public IEnumerable<SeatNewStateModel> Seats { get; set; } = seats;
}

/// <summary>
/// Información de la planta
/// </summary>
public class FloorInformation
{
    /// <summary>
    /// Id
    /// </summary>
    /// <value></value>
    public Guid FloorGuid { get; set; }

    /// <summary>
    /// Identificador de Piso
    /// </summary>
    /// <value></value>
    public string FloorLabel { get; set; }

    /// <summary>
    /// Diagrama
    /// </summary>
    /// <value></value>
    public IEnumerable<RowInformation> Rows { get; set; }
}
/// <summary>
/// Información de la fila
/// </summary>
public class RowInformation
{
    /// <summary>
    /// Espacios
    /// </summary>
    /// <value></value>
    public IEnumerable<BusSpaceInformation> Spaces { get; set; }
}


/// <summary>
/// Espacios de Bus
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="identifier"></param>
/// <param name="busSpaceType"></param>
/// <param name="price"></param>
/// <returns></returns>
public class BusSpaceInformation(
    string identifier,
    BusSpaceType busSpaceType,
    decimal price)
{

    /// <summary>
    /// Identificador de Espacio en el Bus
    /// </summary>
    /// <value></value>
    public string SeatIdentifier { get; set; } = identifier;

    /// <summary>
    /// Tipo de espacio en el Bus
    /// </summary>
    /// <value></value>
    public BusSpaceType BusSpaceType { get; set; } = busSpaceType;

    /// <summary>
    /// Precio del asiento
    /// </summary>
    /// <value></value>
    public decimal Price { get; set; } = price;

}