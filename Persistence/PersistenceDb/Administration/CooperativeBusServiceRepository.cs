using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CooperativeBusServiceRepository(PersistenceContext dbContext, ILogger<CooperativeBusServiceRepository> logger) 
    : GenericRepository<CooperativeBusService>(dbContext, logger), ICooperativeBusServiceRepository
{
}