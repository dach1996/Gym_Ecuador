using LogicApi.Model.Response.Membership;

namespace LogicApi.Model.Request.Membership;

/// <summary>
/// Solicitud para obtener membresías
/// </summary>
public class GetMembershipsRequest : IRequest<GetMembershipsResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio (filtro opcional)
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// Id de la persona (filtro opcional)
    /// </summary>
    public int? PersonId { get; set; }

    /// <summary>
    /// Filtro por estado
    /// </summary>
    public string StatusFilter { get; set; }

    /// <summary>
    /// Filtro por rol en el gimnasio
    /// </summary>
    public string GymRoleFilter { get; set; }

    /// <summary>
    /// Filtro por membresías activas (no vencidas)
    /// </summary>
    public bool? OnlyActiveMemberships { get; set; }

    /// <summary>
    /// Página
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetMembershipsRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetMembershipsRequest()
    {
    }
}
