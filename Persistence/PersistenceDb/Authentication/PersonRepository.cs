using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class PersonRepository(PersistenceContext dbContext, ILogger<PersonRepository> logger) : GenericRepository<Person>(dbContext, logger), IPersonRepository
{
    /// <summary>
    /// Obtiene el Id de una persona por su Guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public async Task<int> GetIdByGuidAsync(Guid guid)
        => await GetFirstOrDefaultGenericAsync(
            select => (int?)select.Id,
            where => where.Guid == guid)
        .ConfigureAwait(false)
        ?? throw new InvalidOperationException($"La persona con Guid: {guid} no existe");
}