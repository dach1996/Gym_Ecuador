using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;
namespace PersistenceDb.Authentication;
public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
{
    public DeviceRepository(PersistenceContext dbContext, ILogger<DeviceRepository> logger) : base(dbContext, logger)
    {
    }
}