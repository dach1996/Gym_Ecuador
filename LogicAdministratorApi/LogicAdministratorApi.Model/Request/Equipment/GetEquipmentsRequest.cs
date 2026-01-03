using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.Equipment;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Equipment;

/// <summary>
/// Solicitud para obtener equipamientos paginados
/// </summary>
public class GetEquipmentsRequest : IApiBaseRequest<GetEquipmentsResponse>
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
    /// Filtro por tipo (código o nombre del tipo)
    /// </summary>
    public string TypeFilter { get; set; }

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

