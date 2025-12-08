using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class PlatformRepository(PersistenceContext dbContext, ILogger<PlatformRepository> logger) 
: GenericRepository<Platform>(dbContext, logger), IPlatformRepository
{
}
