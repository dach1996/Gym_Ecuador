using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymBranchScheduleRepository : GenericRepository<GymBranchSchedule>, IGymBranchScheduleRepository
{
    public GymBranchScheduleRepository(PersistenceContext dbContext, ILogger<GymBranchScheduleRepository> logger) : base(dbContext, logger)
    {
    }
}

