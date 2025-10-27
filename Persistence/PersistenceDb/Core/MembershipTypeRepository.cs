using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class MembershipTypeRepository : GenericRepository<MembershipType>, IMembershipTypeRepository
{
    public MembershipTypeRepository(PersistenceContext dbContext, ILogger<MembershipTypeRepository> logger) : base(dbContext, logger)
    {
    }
}
