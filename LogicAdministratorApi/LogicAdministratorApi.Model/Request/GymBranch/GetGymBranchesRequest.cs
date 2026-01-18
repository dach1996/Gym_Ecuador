using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.GymBranch;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para obtener sucursales de gimnasio paginadas
/// </summary>
public class GetGymBranchesRequest : IPaginatorApiRequest<GetGymBranchesResponse>
{
    /// <summary>
    /// GUID del gimnasio para filtrar las sucursales
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymGuid { get; set; }

    /// <summary>
    /// Filtro
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// Página
        /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamaño de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

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

