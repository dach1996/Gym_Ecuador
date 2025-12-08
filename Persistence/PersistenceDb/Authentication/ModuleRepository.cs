using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class ModuleRepository(PersistenceContext dbContext, ILogger<ModuleRepository> logger) 
: GenericRepository<Module>(dbContext, logger), IModuleRepository
{
}
