using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;
using PersistenceDb.Repository.Interfaces.Authentication;
using PersistenceDb.Utils.Extension;

namespace PersistenceDb.Authentication;

public class RoleRepository(PersistenceContext dbContext, ILogger<RoleRepository> logger)
: GenericRepository<Role>(dbContext, logger), IRoleRepository
{
    /// <summary>
    /// Obtiene los IDs de los roles por el alcance y plataforma
    /// </summary>
    /// <param name="scope"></param>
    /// <param name="platformId"></param>
    /// <returns></returns>
    public async Task<List<int>> GetIdsByScopeAndPlatformAsync(RoleScope scope, RolePlatformType platformType)
    {
        var platformCode = platformType.GetEnumMember();
        return await GetGenericAsync(
            select => select.Id,
            where => where.Scope == scope && where.Platform.Code == platformCode
        ).ConfigureAwait(false);
    }
}
