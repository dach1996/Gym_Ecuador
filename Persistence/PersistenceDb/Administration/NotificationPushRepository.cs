using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class NotificationPushRepository : GenericRepository<NotificationPush>, INotificationPushRepository
{
    public NotificationPushRepository(PersistenceContext dbContext, ILogger<NotificationPushRepository> logger) : base(dbContext, logger)
    {
    }
}