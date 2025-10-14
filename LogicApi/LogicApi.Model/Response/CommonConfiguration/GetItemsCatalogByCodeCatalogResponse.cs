using Common.Messages;
using Common.Utils.CustomExceptions;
using LogicApi.Model.Response.CommonConfiguration.Common;

namespace LogicApi.Model.Response.CommonConfiguration;

/// <summary>
/// Response Obtiene una lista de items de catálogo
/// </summary>
public class GetItemsCatalogByCodeCatalogResponse
{
    /// <summary>
    /// Items de catálogo
    /// </summary>
    private readonly IEnumerable<ItemCatalogResponse> _itemCatalog;

    /// <summary>
    /// Items de catálogo Habilitados
    /// </summary>
    public IEnumerable<ItemCatalogResponse> ItemCatalogsEnabled => _itemCatalog.Where(t => t.Status);

    /// <summary>
    /// Items de catálogo en diccionario
    /// </summary>
    public IDictionary<string, string> DictionaryItemCatalogsEnabled => _itemCatalog.Where(t => t.Status).ToDictionary(t => t.Code, t => t.Value);

    /// <summary>
    /// Obtiene el item de catálogo por el código
    /// </summary>
    /// <param name="itemCatalogCode"></param>
    /// <returns></returns>
    public string GetItemCatalogValudeByCode(string itemCatalogCode)
    {
        var item = _itemCatalog.FirstOrDefault(t => t.Code == itemCatalogCode)
            ?? throw new CustomException((int)MessagesCodesError.ItemCatalogNotFound, $"El Item de catálogo: '{itemCatalogCode}' no encontrado.");
        if (!item.Status)
            throw new CustomException((int)MessagesCodesError.ItemCatalogNotEnabled, $"El Item de catálogo: '{itemCatalogCode}' esta desactivado.");
        return item.Value;
    }

    /// <summary>
    /// Obtiene el item de catálogo por el código
    /// </summary>
    /// <param name="itemCatalogCode"></param>
    /// <returns></returns>
    public ItemCatalogResponse GetItemCatalogByCode(string itemCatalogCode)
    {
        var item = _itemCatalog.FirstOrDefault(t => t.Code == itemCatalogCode)
            ?? throw new CustomException((int)MessagesCodesError.ItemCatalogNotFound, $"El Item de catálogo: '{itemCatalogCode}' no encontrado.");
        if (!item.Status)
            throw new CustomException((int)MessagesCodesError.ItemCatalogNotEnabled, $"El Item de catálogo: '{itemCatalogCode}' esta desactivado.");
        return item;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="itemCatalogs"></param>
    public GetItemsCatalogByCodeCatalogResponse(IEnumerable<ItemCatalogResponse> itemCatalogs) => _itemCatalog = itemCatalogs;
}
