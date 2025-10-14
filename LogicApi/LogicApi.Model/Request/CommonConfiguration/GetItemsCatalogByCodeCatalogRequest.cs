using LogicApi.Model.Response.CommonConfiguration;
using MediatR;

namespace LogicApi.Model.Request.CommonConfiguration;

/// <summary>
/// Request Obtiene una lista de items de catálogo
/// </summary>
public class GetItemsCatalogByCodeCatalogRequest : IRequest<GetItemsCatalogByCodeCatalogResponse>, IApiBaseRequest
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
    public int? TimeSaveInCache{ get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="catalogueCode"></param>
    /// <param name="contextRequest"></param>
    /// <param name="saveInCache"></param>
    public GetItemsCatalogByCodeCatalogRequest(string catalogueCode,  ContextRequest contextRequest, bool saveInCache = true)
    {
        CatalogueCode = catalogueCode;
        SaveInCache = saveInCache;
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
