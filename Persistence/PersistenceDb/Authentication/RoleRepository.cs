using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class RoleRepository(PersistenceContext dbContext, ILogger<RoleRepository> logger) 
: GenericRepository<Role>(dbContext, logger), IRoleRepository
{
}
