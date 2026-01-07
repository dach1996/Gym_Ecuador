using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ClientMembershipRepository : GenericRepository<ClientMembership>, IClientMembershipRepository
{
    public ClientMembershipRepository(PersistenceContext dbContext, ILogger<ClientMembershipRepository> logger) : base(dbContext, logger)
    {
    }
}

