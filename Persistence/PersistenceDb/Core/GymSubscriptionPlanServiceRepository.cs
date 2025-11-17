using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymSubscriptionPlanServiceRepository : GenericRepository<GymSubscriptionPlanService>, IGymSubscriptionPlanServiceRepository
{
    public GymSubscriptionPlanServiceRepository(PersistenceContext dbContext, ILogger<GymSubscriptionPlanServiceRepository> logger) : base(dbContext, logger)
    {
    }
}

