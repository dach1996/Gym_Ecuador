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
    /// Obtiene el ID del rol por el tipo de ambito y plataforma
    /// </summary>
    /// <param name="roleType"></param>
    /// <param name="platformType"></param>
    /// <returns></returns>
    public async Task<int> GetIdByScopeAndPlatformAsync(RoleType roleType, RolePlatformType platformType)
    {
        var roleName = roleType.GetEnumMember();
        var platformCode = platformType.GetEnumMember();
        return await GetFirstOrDefaultGenericAsync(
            select => (int?)select.Id,
            where => where.Name == roleName && where.Platform.Code == platformCode).ConfigureAwait(false)
            ?? throw new InvalidOperationException($"No se encontró el rol con el tipo: {roleType} y plataforma: {platformType}");
    }
}
