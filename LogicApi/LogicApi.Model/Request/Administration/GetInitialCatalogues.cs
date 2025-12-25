using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Models;
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
    public List<CatalogsTypeItemsCodes> ListCatalogsTypeItemsCodes { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="listCatalogsTypeItemsCodes"></param>
    public GetInitialCataloguesRequest(ContextRequest contextRequest, List<CatalogsTypeItemsCodes> listCatalogsTypeItemsCodes = null)
    {
        ContextRequest = contextRequest;
        ListCatalogsTypeItemsCodes = listCatalogsTypeItemsCodes ?? [];
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetInitialCataloguesRequest()
    {

    }
}

/// <summary>
/// Códigos de Catálogos
/// </summary>
public enum CatalogsTypeItemsCodes
{
    /// <summary>
    /// Tipos de Documento
    /// </summary>
    /// <value></value>
    DocumentType = 1,

    /// <summary>
    /// Catálogo de Nacionalidades
    /// </summary>
    /// <value></value>
    Nationality = 2,

    /// <summary>
    /// Catálogo de Géneros
    /// </summary>
    /// <value></value>
    Gender = 3,
}