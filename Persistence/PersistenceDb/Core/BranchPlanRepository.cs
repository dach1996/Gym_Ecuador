using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class BranchPlanRepository : GenericRepository<BranchPlan>, IBranchPlanRepository
{
    public BranchPlanRepository(PersistenceContext dbContext, ILogger<BranchPlanRepository> logger) : base(dbContext, logger)
    {
    }
}

