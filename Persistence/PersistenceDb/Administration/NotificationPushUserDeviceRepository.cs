using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class NotificationPushUserDeviceRepository : GenericRepository<NotificationPushUserDevice>, INotificationPushUserDeviceRepository
{
    public NotificationPushUserDeviceRepository(PersistenceContext dbContext, ILogger<NotificationPushUserDeviceRepository> logger) : base(dbContext, logger)
    {
    }
}

