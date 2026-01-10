using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class RoutineRepository : GenericRepository<Routine>, IRoutineRepository
{
    public RoutineRepository(PersistenceContext dbContext, ILogger<RoutineRepository> logger) : base(dbContext, logger)
    {
    }
}
