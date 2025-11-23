using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.ClassReservation;

using Common.WebCommon.Models;
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
    public CommonContextRequest ContextRequest { get; set; }

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
