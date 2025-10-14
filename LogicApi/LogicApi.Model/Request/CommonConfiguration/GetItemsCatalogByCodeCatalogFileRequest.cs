using LogicApi.Model.Response.CommonConfiguration;
namespace LogicApi.Model.Request.CommonConfiguration;
/// <summary>
/// Request Obtiene una lista de items de catálogo
/// </summary>
public class GetItemsCatalogByCodeCatalogFileRequest : IRequest<GetItemsCatalogByCodeCatalogFileResponse>, IApiBaseRequest
{
    /// <summary>
    /// Código de Catálogo
    /// </summary>
    public string CatalogueCode { get; set; }

    /// <summary>
    /// guardar en Cache
    /// </summary>
    public bool SaveInCache { get; set; }

    /// <summary>
    /// Tiempo a guardar en cache
    /// </summary>
    public int? TimeSaveInCache { get; set; }

    /// <summary>
    /// Tiempo a guardar en cache
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="catalogueCode"></param>
    /// <param name="saveInCache"></param>
    /// <param name="contextRequest"></param>
    /// <param name="language"></param>
    public GetItemsCatalogByCodeCatalogFileRequest(string catalogueCode, string language, ContextRequest contextRequest, bool saveInCache = true)
    {
        CatalogueCode = catalogueCode;
        SaveInCache = saveInCache;
        Language = language;
        ContextRequest = contextRequest;
    }
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
