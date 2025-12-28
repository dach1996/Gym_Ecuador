using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Attributes.CustomDataAnnotations;
using LogicAdministratorApi.Model.Response.Gym;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.Gym;

/// <summary>
/// Solicitud para obtener gimnasios paginados
/// </summary>
public class GetGymsRequest : IApiBaseRequest<GetGymsResponse>
{
    /// <summary>
    /// Filtro por nombre de gimnasio
    /// </summary>
    public string NameFilter { get; set; }

    /// <summary>
    /// Filtro por código de gimnasio
    /// </summary>
    public string CodeFilter { get; set; }

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
    public GetGymsRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetGymsRequest()
    {
    }
}

