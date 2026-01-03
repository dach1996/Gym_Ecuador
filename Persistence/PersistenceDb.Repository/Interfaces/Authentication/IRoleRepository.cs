using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Repository.Interfaces.Authentication;

public interface IRoleRepository : IGenericRepository<Role>
{
    /// <summary>
    /// Obtiene el ID del rol por el tipo de ambito y plataforma
    /// </summary>
    /// <param name="scopeType"></param>
    /// <param name="rolePlatformType"></param>
    /// <returns></returns>
    Task<int> GetIdByScopeAndPlatformAsync(RoleType roleType, RolePlatformType platformType);
}
