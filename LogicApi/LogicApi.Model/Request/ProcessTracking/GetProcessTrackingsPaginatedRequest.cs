using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.ProcessTracking;
using Common.WebCommon.Models;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para obtener seguimientos de proceso paginados
/// </summary>
public class GetProcessTrackingsPaginatedRequest : IRequest<GetProcessTrackingsPaginatedResponse>, IApiBaseRequest
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
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetProcessTrackingsPaginatedRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetProcessTrackingsPaginatedRequest()
    {
    }
}
