using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Service;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Service;

/// <summary>
/// Solicitud para obtener servicios
/// </summary>
public class GetServicesRequest : IRequest<GetServicesResponse>, IApiBaseRequest
{
    /// <summary>
    /// Filtro por nombre
    /// </summary>
    public string NameFilter { get; set; }

    /// <summary>
    /// Filtro por estado activo
    /// </summary>
    public bool? IsActiveFilter { get; set; }

    /// <summary>
    /// Filtro por requiere reserva
    /// </summary>
    public bool? RequiresReservationFilter { get; set; }

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
    public GetServicesRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetServicesRequest()
    {
    }
}

