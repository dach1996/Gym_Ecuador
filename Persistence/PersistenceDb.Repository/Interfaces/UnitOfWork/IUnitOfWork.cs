using PersistenceDb.Repository.Interfaces.Administration;
using PersistenceDb.Repository.Interfaces.Authentication;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Repository.Interfaces.UnitOfWork;

public interface IUnitOfWork : IUnitOfWorkBase
{

    // Administration Repositories
    IAuditLogRepository AuditLogRepository { get; }
    ICatalogRepository CatalogRepository { get; }
    IParameterRepository ParameterRepository { get; }
    IFileRepository FileRepository { get; }
    INotificationPushRepository NotificationPushRepository { get; }
    INotificationPushUserRepository NotificationPushUserRepository { get; }
    ICountryRepository CountryRepository { get; }
    IProvinceRepository ProvinceRepository { get; }
    IRegionRepository RegionRepository { get; }

    // Authentication Repositories
    IDeviceRepository DeviceRepository { get; }
    IUserRepository UserRepository { get; }
    IUserDeviceRepository UserDeviceRepository { get; }
    IPersonRepository PersonRepository { get; }
    IUserRegistrationFormRepository UserRegistrationFormRepository { get; }
    IUserDevicePushTokenRepository UserDevicePushTokenRepository { get; }

    // Core Repositories
    IQueueMessageRepository QueueMessageRepository { get; }
}