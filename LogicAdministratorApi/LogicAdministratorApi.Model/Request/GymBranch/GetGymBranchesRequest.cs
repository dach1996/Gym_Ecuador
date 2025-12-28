using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.GymBranch;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para obtener sucursales de gimnasio paginadas
/// </summary>
public class GetGymBranchesRequest : IApiBaseRequest<GetGymBranchesResponse>
{
    /// <summary>
    /// GUID del gimnasio para filtrar las sucursales
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Filtro por nombre de sucursal
    /// </summary>
    public string NameFilter { get; set; }

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
    public CommonContextRequest ContextRequest { get; set; }

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

