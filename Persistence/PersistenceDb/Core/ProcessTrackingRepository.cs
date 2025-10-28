using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ProcessTrackingRepository : GenericRepository<ProcessTracking>, IProcessTrackingRepository
{
    public ProcessTrackingRepository(PersistenceContext dbContext, ILogger<ProcessTrackingRepository> logger) : base(dbContext, logger)
    {
    }
}
