using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymVideoRepository : GenericRepository<GymVideo>, IGymVideoRepository
{
    public GymVideoRepository(PersistenceContext dbContext, ILogger<GymVideoRepository> logger) : base(dbContext, logger)
    {
    }
}
