using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymSubscriptionPlanRepository : GenericRepository<GymBranchSubscriptionPlan>, IGymSubscriptionPlanRepository
{
    public GymSubscriptionPlanRepository(PersistenceContext dbContext, ILogger<GymSubscriptionPlanRepository> logger) : base(dbContext, logger)
    {
    }
}

