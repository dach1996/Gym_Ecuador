using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class RoutineExerciseRepository : GenericRepository<RoutineExercise>, IRoutineExerciseRepository
{
    public RoutineExerciseRepository(PersistenceContext dbContext, ILogger<RoutineExerciseRepository> logger) : base(dbContext, logger)
    {
    }
}
