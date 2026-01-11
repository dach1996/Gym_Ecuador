using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Repository.Interfaces.Authentication;

public interface IRoleRepository : IGenericRepository<Role>
{
    /// <summary>
    /// Obtiene los IDs de los roles por el alcance y plataforma
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="platformId"></param>
    /// <returns></returns>
    Task<List<int>> GetIdsByScopeAndPlatformAsync(RoleScope scope, RolePlatformType platformType);
}
