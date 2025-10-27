using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GroupClassRepository : GenericRepository<GroupClass>, IGroupClassRepository
{
    public GroupClassRepository(PersistenceContext dbContext, ILogger<GroupClassRepository> logger) : base(dbContext, logger)
    {
    }
}
