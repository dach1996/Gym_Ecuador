using LogicAdministratorApi.Model.Response.Exercise;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicAdministratorApi.Model.Request.Exercise;

/// <summary>
/// Solicitud para obtener ejercicios con paginación
/// </summary>
public class GetExercisesRequest : IApiBaseRequest<GetExercisesResponse>
{
    /// <summary>
    /// Número de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Filtro por nombre (opcional)
    /// </summary>
    public string NameFilter { get; set; }

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
