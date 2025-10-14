namespace LogicApi.Model.Response.Common;
/// <summary>
/// Códigos de Catálogos
/// </summary>
public class CatalogCodes
{
    /// <summary>
    /// Catálogo
    /// </summary>
    /// <value></value>
    public string Catalog { get; set; }

    /// <summary>
    /// Items de catálogo
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> ItemsCatalog { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="itemsCatalog"></param>
    public CatalogCodes(string catalog, IDictionary<string, string> itemsCatalog)
    {
        Catalog = catalog;
        ItemsCatalog = itemsCatalog;
    }
}