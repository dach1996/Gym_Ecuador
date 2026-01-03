using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;
using PersistenceDb.Repository.Interfaces.Authentication;
using PersistenceDb.Utils.Extension;

namespace PersistenceDb.Authentication;

public class UserRoleScopeRepository(PersistenceContext dbContext, ILogger<UserRoleScopeRepository> logger)
: GenericRepository<UserRoleScope>(dbContext, logger), IUserRoleScopeRepository
{
  
}
