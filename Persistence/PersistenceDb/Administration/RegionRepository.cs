using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class RegionRepository(PersistenceContext dbContext, ILogger<RegionRepository> logger) 
    : GenericRepository<Region>(dbContext, logger), IRegionRepository
{
}