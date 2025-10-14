using LogicApi.Model.Response.CommonConfiguration;
namespace LogicApi.Model.Request.CommonConfiguration;

/// <summary>
/// Request para obtener un parámetro
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="parameterCode"></param>
/// <param name="contextRequest"></param>
/// <param name="saveInCache"></param>
public class GetParameterByCodeRequest(string parameterCode, ContextRequest contextRequest, bool saveInCache = true) : IRequest<GetParameterByCodeResponse>, IApiBaseRequest
{
    /// <summary>
    /// Código de Catálogo
    /// </summary>
    public string ParameterCode { get; set; } = parameterCode;

    /// <summary>
    /// guardar en Cache
    /// </summary>
    public bool SaveInCache { get; set; } = saveInCache;

    /// <summary>
    /// Tiempo a guardar en cache
    /// </summary>
    public int? TimeSaveInCache { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; } = contextRequest;
}
