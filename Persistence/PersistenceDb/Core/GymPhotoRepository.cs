using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class GymPhotoRepository : GenericRepository<GymPhoto>, IGymPhotoRepository
{
    public GymPhotoRepository(PersistenceContext dbContext, ILogger<GymPhotoRepository> logger) : base(dbContext, logger)
    {
    }
}
