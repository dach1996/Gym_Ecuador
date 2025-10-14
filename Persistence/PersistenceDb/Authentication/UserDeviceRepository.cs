using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class UserDeviceRepository : GenericRepository<UserDevice>, IUserDeviceRepository
{
    public UserDeviceRepository(PersistenceContext dbContext, ILogger<UserDeviceRepository> logger) : base(dbContext, logger)
    {
    }
}