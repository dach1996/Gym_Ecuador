namespace LogicApi.Model.Response.ClassReservation;

/// <summary>
/// Respuesta de cancelar reserva de clase
/// </summary>
public class CancelClassReservationResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Guid de la reserva de clase cancelada
    /// </summary>
    public Guid ClassReservationGuid { get; set; }

    /// <summary>
    /// Estado de la reserva
    /// </summary>
    public string ReservationStatus { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="classReservationGuid"></param>
    /// <param name="reservationStatus"></param>
    public CancelClassReservationResponse(Guid classReservationGuid, string reservationStatus)
    {
        ClassReservationGuid = classReservationGuid;
        ReservationStatus = reservationStatus;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CancelClassReservationResponse()
    {
    }
}
