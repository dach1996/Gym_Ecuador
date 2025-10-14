using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class StationRepository : GenericRepository<Station>, IStationRepository
{
    public StationRepository(PersistenceContext dbContext, ILogger<StationRepository> logger) : base(dbContext, logger)
    {
    }
}