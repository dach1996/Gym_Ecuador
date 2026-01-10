using LogicApi.Model.Response.Routine;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Routine;

/// <summary>
/// Solicitud para obtener mis rutinas con paginación
/// </summary>
public class GetMyRoutinesRequest : IPaginatorApiRequest<GetMyRoutinesResponse>
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
