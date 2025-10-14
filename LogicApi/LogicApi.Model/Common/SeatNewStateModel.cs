using LogicCommon.Model.Enum;

namespace LogicApi.Model.Common;
/// <summary>
/// Estado de Asiento
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="routeGuid"></param>
/// <param name="floorCooperativeGuid"></param>
/// <param name="seatGuid"></param>
/// <param name="seatState"></param>
/// <param name="seatIdentifier"></param>
/// <param name="userGuid"></param>
public class SeatNewStateModel(
    Guid routeGuid,
    Guid floorCooperativeGuid,
    Guid seatGuid,
    SeatState seatState,
    string seatIdentifier,
    Guid? userGuid)
{
    /// <summary>
    /// Identificador de Usuario
    /// </summary>
    /// <value></value>
    public Guid UserGuid { get; private set; } = userGuid ?? Guid.Empty;

    /// <summary>
    /// Identificador de Piso
    /// </summary>
    /// <value></value>
    public Guid FloorCooperativeGuid { get; private set; } = floorCooperativeGuid;

    /// <summary>
    /// Identificador de Asiento
    /// </summary>
    /// <value></value>
    public string SeatIdentifier { get; private set; } = seatIdentifier;

    /// <summary>
    /// Identificador  de Ticket
    /// </summary>
    /// <value></value>
    public Guid RouteGuid { get; private set; } = routeGuid;

    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public SeatState SeatState { get; private set; } = seatState;

    /// <summary>
    /// Identificador de Asiento
    /// </summary>
    /// <value></value>
    public Guid SeatGuid { get; private set; } = seatGuid;

}