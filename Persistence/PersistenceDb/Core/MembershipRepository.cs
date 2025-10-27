using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
{
    public MembershipRepository(PersistenceContext dbContext, ILogger<MembershipRepository> logger) : base(dbContext, logger)
    {
    }
}
