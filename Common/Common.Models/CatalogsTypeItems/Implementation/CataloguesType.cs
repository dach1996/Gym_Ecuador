using System.Text;
using Common.Models.CatalogsTypeItems.Interface;
using Common.Models.CatalogsTypeItems.Models;
using Newtonsoft.Json;

namespace Common.Models.CatalogsTypeItems.Implementation;
/// <summary>
/// Implementación de Items de Catalogue Types
/// </summary>
public class CataloguesType : ICataloguesType
{
    private readonly IEnumerable<CatalogModel> listCatalogModel;

    /// <summary>
    /// Constructo
    /// </summary>
    /// <param name="fileName"></param>
    public CataloguesType(string fileName)
    {
        var pathFile = Path.Combine(AppContext.BaseDirectory, fileName);
        if (!File.Exists(pathFile))
            throw new FileNotFoundException($"No se pudo encontrar el archivo '{fileName}'", fileName);
        var json = File.ReadAllText(pathFile, Encoding.UTF8);
        listCatalogModel = JsonConvert.DeserializeObject<IEnumerable<CatalogModel>>(json);
    }

    /// <summary>
    /// Obtiene un catálogo mediante el código
    /// </summary>
    /// <param name="catalogCode"></param>
    /// <returns></returns>

    public async Task<CatalogModel> GetCatalogByCodeAsync(string catalogCode)
        => await Task.Run(() => listCatalogModel.FirstOrDefault(t => t.Code == catalogCode)
            ?? throw new KeyNotFoundException($"No se encuentra el código '{catalogCode}'")).ConfigureAwait(false);

    /// <summary>
    /// Obtiene un diccionario de items de catálogo con el lenguaje seleccionado
    /// </summary>
    /// <param name="catalogCode"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public async Task<IDictionary<string, string>> GetItemsCatalogByCatalogCodeAsync(string catalogCode, string language) => (await GetCatalogByCodeAsync(catalogCode).ConfigureAwait(false))
                ?.ListItemCatalog
                .Select(t => new KeyValuePair<string, string>(t.Code, t.LanguageValue?.FirstOrDefault(h => h.Key == language).Value))
                .ToDictionary(x => x.Key, x => x.Value);
}