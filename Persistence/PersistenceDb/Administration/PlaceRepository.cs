using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
{
    public PlaceRepository(PersistenceContext dbContext, ILogger<PlaceRepository> logger) : base(dbContext, logger)
    {
    }
}