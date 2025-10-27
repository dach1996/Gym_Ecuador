using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ClassScheduleRepository : GenericRepository<ClassSchedule>, IClassScheduleRepository
{
    public ClassScheduleRepository(PersistenceContext dbContext, ILogger<ClassScheduleRepository> logger) : base(dbContext, logger)
    {
    }
}
