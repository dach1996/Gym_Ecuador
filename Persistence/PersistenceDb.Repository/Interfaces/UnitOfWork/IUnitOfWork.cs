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
    IProcessTrackingRepository ProcessTrackingRepository { get; }
    IProcessTrackingImageRepository ProcessTrackingImageRepository { get; }

    // Gym Repositories
    IGymRepository GymRepository { get; }
    IMembershipTypeRepository MembershipTypeRepository { get; }
    IMembershipRepository MembershipRepository { get; }
    IGymPhotoRepository GymPhotoRepository { get; }
    IGymVideoRepository GymVideoRepository { get; }
    IGymMachineRepository GymMachineRepository { get; }
    ITrainerRepository TrainerRepository { get; }
    ITrainerGymRepository TrainerGymRepository { get; }
    IGroupClassRepository GroupClassRepository { get; }
    IClassScheduleRepository ClassScheduleRepository { get; }
    IClassReservationRepository ClassReservationRepository { get; }
    IGymReviewRepository GymReviewRepository { get; }
    ITrainerRatingRepository TrainerRatingRepository { get; }
    IPersonalGoalRepository PersonalGoalRepository { get; }
}