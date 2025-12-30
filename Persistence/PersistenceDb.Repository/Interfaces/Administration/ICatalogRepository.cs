using PersistenceDb.Models.Administration;

namespace PersistenceDb.Repository.Interfaces.Administration;

public interface ICatalogRepository : IGenericRepository<Catalog>
{
    /// <summary>
    /// Obtiene el catalogo por su código
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    Task<int> GetIdByCodeAsync(string code);
}