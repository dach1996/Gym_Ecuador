using LogicApi.Model.Response.Exercise;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Exercise;

/// <summary>
/// Solicitud para obtener ejercicios con paginación
/// </summary>
public class GetExercisesRequest : IPaginatorApiRequest<GetExercisesResponse>
{
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
    /// Filtro por tag/categoría (opcional)
    /// </summary>
    public int? CatalogId { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
