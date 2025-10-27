using LogicApi.Model.Response.Membership;

namespace LogicApi.Model.Request.Membership;

/// <summary>
/// Solicitud para crear una membresía
/// </summary>
public class CreateMembershipRequest : IRequest<CreateMembershipResponse>, IApiBaseRequest
{
    /// <summary>
    /// Id de la persona
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Id del tipo de membresía
    /// </summary>
    public int MembershipTypeId { get; set; }

    /// <summary>
    /// Fecha de inicio
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }

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
    public CreateMembershipRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateMembershipRequest()
    {
    }
}
