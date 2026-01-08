using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.BranchPlan;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.BranchPlan;

/// <summary>
/// Solicitud para obtener planes de sucursal paginados
/// </summary>
public class GetBranchPlansRequest : IPaginatorApiRequest<GetBranchPlansResponse>
{
    /// <summary>
    /// GUID de la sucursal de gimnasio
    /// </summary>
    [Required]
    [ValidateGuid]
    public Guid GymBranchGuid { get; set; }

    /// <summary>
    /// Número de página
    /// </summary>
    [Required]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    [Required]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Filtro por nombre
    /// </summary>
    public string NameFilter { get; set; }

    /// <summary>
    /// Filtro por código
    /// </summary>
    public string CodeFilter { get; set; }

    /// <summary>
    /// Filtro por estado activo/inactivo
    /// </summary>
    public bool? IsActiveFilter { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

