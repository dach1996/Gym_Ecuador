using LogicApi.Model.Response.CommonConfiguration.Common;

namespace LogicApi.Model.Response.CommonConfiguration;

/// <summary>
/// Response Obtiene una lista de items de catálogo
/// </summary>
public class GetItemsCatalogByCodeCatalogFileResponse
{
    /// <summary>
    /// items de catálogo
    /// </summary>
    public List<ItemCatalogFileResponse> ItemCatalogs { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="itemCatalogs"></param>
    public GetItemsCatalogByCodeCatalogFileResponse(List<ItemCatalogFileResponse> itemCatalogs) 
        => ItemCatalogs = itemCatalogs;
}
