using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class FunctionalityRepository(PersistenceContext dbContext, ILogger<FunctionalityRepository> logger) 
: GenericRepository<Functionality>(dbContext, logger), IFunctionalityRepository
{
}
