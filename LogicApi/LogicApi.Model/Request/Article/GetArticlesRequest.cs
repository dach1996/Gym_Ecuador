using LogicApi.Model.Response.Article;
using Common.WebCommon.Models;

namespace LogicApi.Model.Request.Article;

/// <summary>
/// Solicitud para obtener artículos paginados
/// </summary>
public class GetArticlesRequest : IPaginatorApiRequest<GetArticlesResponse>
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
    /// Filtro por categoría (opcional)
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Filtro por título (opcional)
    /// </summary>
    public string TitleFilter { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

