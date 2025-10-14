using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CooperativeBusRepository : GenericRepository<CooperativeBus>, ICooperativeBusRepository
{
    public CooperativeBusRepository(PersistenceContext dbContext, ILogger<CooperativeBusRepository> logger) : base(dbContext, logger)
    {
    }
}