using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class TransportPointRepository(PersistenceContext dbContext, ILogger<TransportPointRepository> logger) 
    : GenericRepository<TransportPoint>(dbContext, logger), ITransportPointRepository
{
}