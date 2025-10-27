namespace LogicApi.Model.Response.ClassReservation;

/// <summary>
/// Respuesta de crear reserva de clase
/// </summary>
public class CreateClassReservationResponse : IApiBaseResponse
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
    /// Guid de la reserva de clase creada
    /// </summary>
    public Guid ClassReservationGuid { get; set; }

    /// <summary>
    /// Fecha de reserva
    /// </summary>
    public DateTime ReservationDate { get; set; }

    /// <summary>
    /// Estado de la reserva
    /// </summary>
    public string ReservationStatus { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="classReservationGuid"></param>
    /// <param name="reservationDate"></param>
    /// <param name="reservationStatus"></param>
    public CreateClassReservationResponse(Guid classReservationGuid, DateTime reservationDate, string reservationStatus)
    {
        ClassReservationGuid = classReservationGuid;
        ReservationDate = reservationDate;
        ReservationStatus = reservationStatus;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateClassReservationResponse()
    {
    }
}
