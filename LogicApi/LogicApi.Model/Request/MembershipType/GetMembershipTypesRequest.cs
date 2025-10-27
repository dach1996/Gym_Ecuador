using LogicApi.Model.Response.MembershipType;

namespace LogicApi.Model.Request.MembershipType;

/// <summary>
/// Solicitud para obtener tipos de membresía
/// </summary>
public class GetMembershipTypesRequest : IRequest<GetMembershipTypesResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio (filtro opcional)
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// Filtro por estado activo
    /// </summary>
    public bool? IsActiveFilter { get; set; }

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
    public GetMembershipTypesRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetMembershipTypesRequest()
    {
    }
}
