using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ProcessTrackingMeasurementRepository : GenericRepository<ProcessTrackingMeasurement>, IProcessTrackingMeasurementRepository
{
    public ProcessTrackingMeasurementRepository(PersistenceContext dbContext, ILogger<ProcessTrackingMeasurementRepository> logger) : base(dbContext, logger)
    {
    }
}
