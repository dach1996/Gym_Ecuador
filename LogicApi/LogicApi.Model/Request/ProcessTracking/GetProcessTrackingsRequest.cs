using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para obtener seguimientos de procesos
/// </summary>
public class GetProcessTrackingsRequest : IRequest<GetProcessTrackingsResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid del gimnasio (filtro opcional)
    /// </summary>
    public Guid? GymGuid { get; set; }

    /// <summary>
    /// Filtro por tipo de proceso
    /// </summary>
    public string ProcessTypeFilter { get; set; }

    /// <summary>
    /// Filtro por estado
    /// </summary>
    public string StatusFilter { get; set; }

    /// <summary>
    /// Filtro por estado activo
    /// </summary>
    public bool? IsActiveFilter { get; set; }

    /// <summary>
    /// Página
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Tamaño de página
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetProcessTrackingsRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetProcessTrackingsRequest()
    {
    }
}
