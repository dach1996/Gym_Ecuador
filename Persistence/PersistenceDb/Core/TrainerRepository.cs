using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
{
    public TrainerRepository(PersistenceContext dbContext, ILogger<TrainerRepository> logger) : base(dbContext, logger)
    {
    }
}
