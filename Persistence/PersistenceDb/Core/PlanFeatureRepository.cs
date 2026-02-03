using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class PlanFeatureRepository : GenericRepository<PlanFeature>, IPlanFeatureRepository
{
    public PlanFeatureRepository(PersistenceContext dbContext, ILogger<PlanFeatureRepository> logger) : base(dbContext, logger)
    {
    }
}
