using LogicApi.Model.Response.SeriesRecord;
using Common.WebCommon.Models;
using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.SeriesRecord;

/// <summary>
/// Solicitud para obtener historial de registros de series con paginación
/// </summary>
public class GetSeriesRecordsHistoryRequest : IPaginatorApiRequest<GetSeriesRecordsHistoryResponse>
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
    /// Filtro por ejercicio (opcional)
    /// </summary>
    [ValidateGuid]
    public Guid? ExerciseGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
