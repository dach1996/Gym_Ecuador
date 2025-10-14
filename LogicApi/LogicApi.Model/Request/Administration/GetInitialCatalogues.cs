using LogicApi.Model.Response.Administration;
namespace LogicApi.Model.Request.Administration;
/// <summary>
/// Obtiene los catálogos iniciales
/// </summary>
public class GetInitialCataloguesRequest : IRequest<GetInitialCataloguesResponse>, IApiBaseRequest
{
    /// <summary>
    /// Códito de Catálogos
    /// </summary>
    /// <value></value>
    public List<string> ListCatalogsTypeItemsCodes { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="listCatalogsTypeItemsCodes"></param>
    public GetInitialCataloguesRequest(ContextRequest contextRequest, List<string> listCatalogsTypeItemsCodes = null)
    {
        ContextRequest = contextRequest;
        ListCatalogsTypeItemsCodes = listCatalogsTypeItemsCodes;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetInitialCataloguesRequest()
    {

    }
}