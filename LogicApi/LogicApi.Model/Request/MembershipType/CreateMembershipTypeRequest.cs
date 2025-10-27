using LogicApi.Model.Response.MembershipType;

namespace LogicApi.Model.Request.MembershipType;

/// <summary>
/// Solicitud para crear un tipo de membresía
/// </summary>
public class CreateMembershipTypeRequest : IRequest<CreateMembershipTypeResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio
    /// </summary>
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Duración en días
    /// </summary>
    public int DurationDays { get; set; }

    /// <summary>
    /// Descripción de beneficios
    /// </summary>
    public string BenefitsDescription { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public CreateMembershipTypeRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateMembershipTypeRequest()
    {
    }
}
