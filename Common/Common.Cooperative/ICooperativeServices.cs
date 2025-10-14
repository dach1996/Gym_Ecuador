using Common.Cooperative.Models.Request;
using Common.Cooperative.Models.Response;

namespace Common.Cooperative;
/// <summary>
/// Servicios de Cooperativas
/// </summary>
public interface ICooperativeServices
{
    /// <summary>
    /// Obtiene los viajes disponibles
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetAvailableTripsResponse> GetAvailableTripsAsync(GetAvailableTripsRequest request);

    /// <summary>
    /// Obtiene  puestos del bus disponible
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<GetBusSeatResponse> GetBusSeatAsync(GetBusSeatRequest request);

    /// <summary>
    /// Reserva Asiento
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ReserveSeatResponse> ReserveSeatAsync(ReserveSeatRequest request);
}