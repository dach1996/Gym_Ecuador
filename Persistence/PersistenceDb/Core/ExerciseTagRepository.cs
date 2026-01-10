using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ExerciseTagRepository : GenericRepository<ExerciseTag>, IExerciseTagRepository
{
    public ExerciseTagRepository(PersistenceContext dbContext, ILogger<ExerciseTagRepository> logger) : base(dbContext, logger)
    {
    }
}
