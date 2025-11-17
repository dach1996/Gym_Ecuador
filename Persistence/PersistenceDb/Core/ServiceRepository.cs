using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository
{
    public ServiceRepository(PersistenceContext dbContext, ILogger<ServiceRepository> logger) : base(dbContext, logger)
    {
    }
}

