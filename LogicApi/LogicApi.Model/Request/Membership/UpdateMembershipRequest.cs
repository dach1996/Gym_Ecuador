using LogicApi.Model.Response.Membership;

namespace LogicApi.Model.Request.Membership;

/// <summary>
/// Solicitud para actualizar una membresía
/// </summary>
public class UpdateMembershipRequest : IRequest<UpdateMembershipResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid de la membresía
    /// </summary>
    public Guid MembershipGuid { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Estado de la membresía
    /// </summary>
    public string Status { get; set; } // Activa, Vencida, Congelada

    /// <summary>
    /// Rol en el gimnasio
    /// </summary>
    public string GymRole { get; set; } // Miembro, Entrenador, Administrador

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public UpdateMembershipRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public UpdateMembershipRequest()
    {
    }
}
