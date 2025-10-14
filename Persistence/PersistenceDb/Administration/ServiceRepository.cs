using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(PersistenceContext dbContext, ILogger<ServiceRepository> logger) : base(dbContext, logger)
    {
    }
}