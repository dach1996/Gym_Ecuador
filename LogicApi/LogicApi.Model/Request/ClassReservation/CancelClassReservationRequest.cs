using LogicApi.Model.Response.ClassReservation;

namespace LogicApi.Model.Request.ClassReservation;

/// <summary>
/// Solicitud para cancelar una reserva de clase
/// </summary>
public class CancelClassReservationRequest : IRequest<CancelClassReservationResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid de la reserva de clase
    /// </summary>
    public Guid ClassReservationGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CancelClassReservationRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CancelClassReservationRequest()
    {
    }
}
