namespace LogicCommon.Model.Common.Hub;
/// <summary>
/// Modelo de Hub para actualizar asiento
/// </summary>
public class UpdateSeatHubModel : IHubModel
{
    /// <summary>
    /// Items a actualizar
    /// </summary>
    /// <value></value>
    public IEnumerable<UpdateSeatItemHubModel> BusReverdedSpaces { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="updateSeats"></param>
    public UpdateSeatHubModel(IEnumerable<UpdateSeatItemHubModel> updateSeats) => BusReverdedSpaces = updateSeats;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="updateSeat"></param>
    public UpdateSeatHubModel(UpdateSeatItemHubModel updateSeat) => BusReverdedSpaces = [updateSeat];

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="floorCooperativeGuid"> Identificador de Piso </param>
    /// <param name="routeGuid"> Identificador de Ruta </param>
    /// <param name="seatGuid"> Identificador de Asiento </param>
    /// <param name="seatIdentifier"> Identificador de Asiento </param>
    /// <param name="seatState"> Estado de Asiento </param>
    /// <param name="userGuid"> Identificador de Usuario </param>
    public UpdateSeatHubModel(
        Guid routeGuid,
        Guid floorCooperativeGuid,
        Guid seatGuid,
        string seatIdentifier,
        string seatState,
        Guid? userGuid = null
        )
    {
        BusReverdedSpaces = [new(
         routeGuid,
         floorCooperativeGuid,
         seatGuid,
         seatIdentifier,
         seatState,
         userGuid
         )
    ];
    }
}

/// <summary>
/// Constructor
/// </summary>
/// <param name="userGuid"></param>
/// <param name="floorCooperativeId"></param>
/// <param name="seatIdentifier"></param>
/// <param name="seatState"></param>
/// <param name="ticketIdentifier"></param>
/// <param name="seatId"></param>
public class UpdateSeatItemHubModel(
    Guid routeGuid,
    Guid floorCooperativeGuid,
    Guid seatGuid,
    string seatIdentifier,
    string seatState,
    Guid? userGuid = null
    )
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
    /// Identificador  de Ruta
    /// </summary>
    /// <value></value>
    public Guid RouteGuid { get; private set; } = routeGuid;

    /// <summary>
    /// Id de Asiento
    /// </summary>
    /// <value></value>
    public Guid SeatGuid { get; set; } = seatGuid;

    /// <summary>
    /// Identificador de Asiento
    /// </summary>
    /// <value></value>
    public string SeatIdentifier { get; private set; } = seatIdentifier;

    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public string SeatState { get; private set; } = seatState;
}