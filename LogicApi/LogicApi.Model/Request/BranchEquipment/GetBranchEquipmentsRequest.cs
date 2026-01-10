using LogicApi.Model.Response.BranchEquipment;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.BranchEquipment;

/// <summary>
/// Solicitud para obtener equipos de sucursal paginados
/// </summary>
public class GetBranchEquipmentsRequest : IPaginatorApiRequest<GetBranchEquipmentsResponse>
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
}

