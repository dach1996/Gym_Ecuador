using LogicCommon.Model.Response.Administration;
using System.Text.Json.Serialization;

namespace LogicCommon.Model.Request.Administration;
/// <summary>
/// Request para obtener los catálogos iniciales
/// </summary>
public class GetInitialCataloguesCommonRequest : ICommonBaseRequest<GetInitialCataloguesResponse>
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
    public CommonContextRequest CommonContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="listCatalogsTypeItemsCodes"></param>
    public GetInitialCataloguesCommonRequest(CommonContextRequest commonContextRequest, List<CatalogsTypeItemsCodes> listCatalogsTypeItemsCodes = null)
    {
        CommonContextRequest = commonContextRequest;
        ListCatalogsTypeItemsCodes = listCatalogsTypeItemsCodes ?? [];
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetInitialCataloguesCommonRequest()
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

    /// <summary>
    /// Catálogo de Tipos de Equipamiento Gimnasio
    /// </summary>
    /// <value></value>
    EquipmentTypeGym = 4,
}

