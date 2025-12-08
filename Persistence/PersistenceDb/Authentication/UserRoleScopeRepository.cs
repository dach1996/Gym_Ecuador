using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class UserRoleScopeRepository(PersistenceContext dbContext, ILogger<UserRoleScopeRepository> logger) 
: GenericRepository<UserRoleScope>(dbContext, logger), IUserRoleScopeRepository
{
}
