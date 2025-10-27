using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymRepository : GenericRepository<Gym>, IGymRepository
{
    public GymRepository(PersistenceContext dbContext, ILogger<GymRepository> logger) : base(dbContext, logger)
    {
    }
}
