using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class UserDevicePushTokenRepository : GenericRepository<UserDevicePushToken>, IUserDevicePushTokenRepository
{
    public UserDevicePushTokenRepository(PersistenceContext dbContext, ILogger<UserDevicePushTokenRepository> logger) : base(dbContext, logger)
    {
    }
}