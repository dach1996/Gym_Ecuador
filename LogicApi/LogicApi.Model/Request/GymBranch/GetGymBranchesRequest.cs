using LogicApi.Model.Response.GymBranch;

namespace LogicApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para obtener sucursales de gimnasio paginadas
/// </summary>
public class GetGymBranchesRequest : IRequest<GetGymBranchesResponse>, IApiBaseRequest
{
    /// <summary>
    /// GUID del gimnasio principal (opcional)
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// Filtro por nombre de sucursal
    /// </summary>
    public string NameFilter { get; set; }

    /// <summary>
    /// Filtro por estado activo
    /// </summary>
    public bool? IsActiveFilter { get; set; }

    /// <summary>
    /// Filtro por código de sucursal
    /// </summary>
    public string CodeFilter { get; set; }

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
    public GetGymBranchesRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymBranchesRequest()
    {
    }
}

