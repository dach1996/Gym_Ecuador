using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymBranchImageRepository : GenericRepository<GymBranchImage>, IGymBranchImageRepository
{
    public GymBranchImageRepository(PersistenceContext dbContext, ILogger<GymBranchImageRepository> logger) : base(dbContext, logger)
    {
    }
}

