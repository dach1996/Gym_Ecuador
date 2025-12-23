using LogicApi.Model.Response.GymBranch;
using Common.WebCommon.Models;
using LogicApi.Model.Common;
namespace LogicApi.Model.Request.GymBranch;

/// <summary>
/// Solicitud para obtener sucursales de gimnasio paginadas
/// </summary>
public class GetGymBranchesRequest : IPaginatorApiRequest<GetGymBranchesResponse>
{
    /// <summary>
    /// GUID del gimnasio principal (opcional)
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// Latitud y longitud
    /// </summary>
    public LatitudeLongitudeModel LatitudeLongitude { get; set; }

    /// <summary>
    /// Servicios
    /// </summary>
    /// <value></value>
    public List<string> Services { get; set; } = [];

    /// <summary>
    /// Tipos de gimnasio
    /// </summary>
    /// <value></value>
    public List<string> GymTypes { get; set; } = [];

    /// <summary>
    /// Indica si la sucursal está abierta en este momento
    /// </summary>
    /// <value></value>
    public bool IsOpenNow { get; set; }

    /// <summary>
    /// Filtro de rango de precios
    /// </summary>
    /// <value></value>
    public PriceRangeFilter PriceRangeFilter { get; set; }

    /// <summary>
    /// Número de página
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }


    /// <summary>
    /// Tamaño de página
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

/// <summary>
/// Filtro de rango de precios  
/// /// </summary>
public class PriceRangeFilter
{
    /// <summary>
    /// Precio mínimo
    /// </summary>
    /// <value></value>
    public decimal MinPrice { get; set; }

    /// <summary>
    /// Precio máximo
    /// </summary>
    /// <value></value>
    public decimal MaxPrice { get; set; }
}