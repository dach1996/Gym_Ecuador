using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class RoleFunctionalityRepository(PersistenceContext dbContext, ILogger<RoleFunctionalityRepository> logger) 
: GenericRepository<RoleFunctionality>(dbContext, logger), IRoleFunctionalityRepository
{
}
