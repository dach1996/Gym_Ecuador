using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class CooperativeRouteRepository(PersistenceContext dbContext, ILogger<CooperativeRouteRepository> logger)
: GenericRepository<CooperativeRoute>(dbContext, logger), ICooperativeRouteRepository
{
}