using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ProcessTrackingImageRepository : GenericRepository<ProcessTrackingImage>, IProcessTrackingImageRepository
{
    public ProcessTrackingImageRepository(PersistenceContext dbContext, ILogger<ProcessTrackingImageRepository> logger) : base(dbContext, logger)
    {
    }
}

