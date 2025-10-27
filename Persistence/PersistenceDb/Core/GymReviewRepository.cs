using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymReviewRepository : GenericRepository<GymReview>, IGymReviewRepository
{
    public GymReviewRepository(PersistenceContext dbContext, ILogger<GymReviewRepository> logger) : base(dbContext, logger)
    {
    }
}
