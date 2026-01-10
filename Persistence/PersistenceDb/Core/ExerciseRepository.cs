using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ExerciseRepository : GenericRepository<Exercise>, IExerciseRepository
{
    public ExerciseRepository(PersistenceContext dbContext, ILogger<ExerciseRepository> logger) : base(dbContext, logger)
    {
    }
}
