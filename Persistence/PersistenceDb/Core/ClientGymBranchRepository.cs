using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ClientGymBranchRepository : GenericRepository<ClientGymBranch>, IClientGymBranchRepository
{
    public ClientGymBranchRepository(PersistenceContext dbContext, ILogger<ClientGymBranchRepository> logger) : base(dbContext, logger)
    {
    }
}

