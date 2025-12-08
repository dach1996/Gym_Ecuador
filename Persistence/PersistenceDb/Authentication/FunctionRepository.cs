using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class FunctionRepository(PersistenceContext dbContext, ILogger<FunctionRepository> logger) 
: GenericRepository<Function>(dbContext, logger), IFunctionRepository
{
}
