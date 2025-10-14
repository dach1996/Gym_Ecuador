using Common.Models.CatalogsTypeItems.Models;
namespace Common.Models.CatalogsTypeItems.Interface;

/// <summary>
/// Tipos de Códigos
/// </summary>
public interface ICataloguesType
{
    /// <summary>
    /// Obtiene un catálogo mediante el código
    /// </summary>
    /// <param name="catalogCode"></param>
    /// <returns></returns>
    Task<CatalogModel> GetCatalogByCodeAsync(string catalogCode);

    /// <summary>
    /// Obtiene un diccionario con los códigos de un catálogo
    /// </summary>
    /// <param name="catalogCode"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    Task<IDictionary<string, string>> GetItemsCatalogByCatalogCodeAsync(string catalogCode, string language);
}