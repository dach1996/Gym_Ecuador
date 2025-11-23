using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.ClassReservation;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.ClassReservation;

/// <summary>
/// Solicitud para crear una reserva de clase
/// </summary>
public class CreateClassReservationRequest : IRequest<CreateClassReservationResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id del horario de clase
    /// </summary>
    public int ClassScheduleId { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Fecha de reserva
    /// </summary>
    public DateTime ReservationDate { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateClassReservationRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateClassReservationRequest()
    {
    }
}
