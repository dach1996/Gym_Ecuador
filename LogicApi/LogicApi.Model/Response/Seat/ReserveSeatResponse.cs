using LogicApi.Model.Common;

namespace LogicApi.Model.Response.Seat;
/// <summary>
/// Asientos Disponibles
/// </summary>
public class ReserveSeatResponse
{
    /// <summary>
    /// Estado de Asiento
    /// </summary>
    /// <value></value>
    public SeatNewStateModel SeatNewState { get; set; }
}

