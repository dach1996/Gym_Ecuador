namespace Common.Models.CatalogsTypeItems.Models;
/// <summary>
/// Modelo de Catálogo
/// </summary>
public class CatalogModel
{
    /// <summary>
    /// Código de Catálogo
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Lista de Items de Catálogo
    /// </summary>
    /// <value></value>
    public IEnumerable<ItemCatalogModel> ListItemCatalog { get; set; }
}

/// <summary>
/// Items de Catálogo
/// </summary>
public class ItemCatalogModel
{
    /// <summary>
    /// Código de Item de Catálogo
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    /// <value></value>
    /// 
    public string Description { get; set; }
    
    /// <summary>
    /// Enumerable aplicable
    /// </summary>
    /// <value></value>
    public string Enum { get; set; }

    /// <summary>
    /// Valor por Lenguage
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> LanguageValue { get; set; }

}