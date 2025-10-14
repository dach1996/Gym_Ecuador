using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CooperativeTransportPointRepository(PersistenceContext dbContext, ILogger<CooperativeTransportPointRepository> logger) 
    : GenericRepository<CooperativeTransportPoint>(dbContext, logger), ICooperativeTransportPointRepository
{
}