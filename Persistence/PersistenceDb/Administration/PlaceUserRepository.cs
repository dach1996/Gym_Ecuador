using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class PlaceUserRepository : GenericRepository<PlaceUser>, IPlaceUserRepository
{
    public PlaceUserRepository(PersistenceContext dbContext, ILogger<PlaceUserRepository> logger) : base(dbContext, logger)
    {
    }
}