using LogicApi.Model.Response.Gym;

namespace LogicApi.Model.Request.Gym;

/// <summary>
/// Solicitud para obtener gimnasios
/// </summary>
public class GetGymsRequest : IRequest<GetGymsResponse>, IApiBaseRequest
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
