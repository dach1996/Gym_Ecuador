using Common.WebCommon.Models.Enum;
using LogicApi.Model.Response.Place;
namespace LogicApi.Model.Request.Place;
/// <summary>
/// Request para obtener los lugares favoritos de un usuario
/// </summary>
public class GetPlacesPaginatedRequest : IPaginatorSortApiRequest<GetPlacesPaginatedResponse>
{
    /// <summary>
    /// Tamaño de la página
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

    /// <summary>
    /// Número de Página
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// Tipo de ordenamiento
    /// </summary>
    /// <value></value>
    [Required]
    public SortableType SortableType { get; set; } = SortableType.Desc;

    
    /// <summary>
    /// Propiedades para buscar
    /// </summary>
    /// <value></value>
    public string PropertySearch { get; set; }
    
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
