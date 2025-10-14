using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class NotificationPushUserRepository : GenericRepository<NotificationPushUser>, INotificationPushUserRepository
{
    public NotificationPushUserRepository(PersistenceContext dbContext, ILogger<NotificationPushUserRepository> logger) : base(dbContext, logger)
    {
    }
}