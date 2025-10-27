using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class TrainerRatingRepository : GenericRepository<TrainerRating>, ITrainerRatingRepository
{
    public TrainerRatingRepository(PersistenceContext dbContext, ILogger<TrainerRatingRepository> logger) : base(dbContext, logger)
    {
    }
}
