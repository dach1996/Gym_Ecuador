using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class TrainerGymRepository : GenericRepository<TrainerGym>, ITrainerGymRepository
{
    public TrainerGymRepository(PersistenceContext dbContext, ILogger<TrainerGymRepository> logger) : base(dbContext, logger)
    {
    }
}

