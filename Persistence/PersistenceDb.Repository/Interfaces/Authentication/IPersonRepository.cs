using PersistenceDb.Models.Authentication;
namespace PersistenceDb.Repository.Interfaces.Authentication;
public interface IPersonRepository : IGenericRepository<Person>
{
    /// <summary>
    /// Obtiene el Id de una persona por su Guid
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    Task<int> GetIdByGuidAsync(Guid guid);
}