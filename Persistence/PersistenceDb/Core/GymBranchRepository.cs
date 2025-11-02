using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymBranchRepository : GenericRepository<GymBranch>, IGymBranchRepository
{
    public GymBranchRepository(PersistenceContext dbContext, ILogger<GymBranchRepository> logger) : base(dbContext, logger)
    {
    }
}

