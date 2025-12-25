using LogicApi.Model.Response.Forum;
using Common.WebCommon.Models;

namespace LogicApi.Model.Request.Forum;

/// <summary>
/// Solicitud para obtener foros paginados
/// </summary>
public class GetForumsRequest : IPaginatorApiRequest<GetForumsResponse>
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
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

