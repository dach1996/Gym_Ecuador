using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymMachineRepository : GenericRepository<GymMachine>, IGymMachineRepository
{
    public GymMachineRepository(PersistenceContext dbContext, ILogger<GymMachineRepository> logger) : base(dbContext, logger)
    {
    }
}
