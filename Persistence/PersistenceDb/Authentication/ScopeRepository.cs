using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class ScopeRepository(PersistenceContext dbContext, ILogger<ScopeRepository> logger) 
: GenericRepository<Scope>(dbContext, logger), IScopeRepository
{
}
