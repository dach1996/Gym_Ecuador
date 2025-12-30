using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CatalogRepository(PersistenceContext dbContext, ILogger<CatalogRepository> logger)
    : GenericRepository<Catalog>(dbContext, logger), ICatalogRepository
{
    /// <summary>
    /// Obtiene el catalogo por su código
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<int> GetIdByCodeAsync(string code)
    {
        var catalog = await GetFirstOrDefaultGenericAsync(select => new { select.Id }, where => where.Code == code)
            .ConfigureAwait(false) ?? throw new InvalidOperationException($"El catalogo con código: {code} no existe");
        return catalog.Id;
    }
}