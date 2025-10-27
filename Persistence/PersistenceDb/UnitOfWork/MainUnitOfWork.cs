using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersistenceDb.Administration;
using PersistenceDb.Authentication;
using PersistenceDb.Core;
using PersistenceDb.Repository.Interfaces.Administration;
using PersistenceDb.Repository.Interfaces.Authentication;
using PersistenceDb.Repository.Interfaces.Core;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace PersistenceDb.UnitOfWork;

public class MainUnitOfWork(
    ILoggerFactory loggerFactory,
    IConfiguration configuration,
    IDbContextFactory<PersistenceContext> dbContextFactory) : UnitOfWork(loggerFactory, configuration, dbContextFactory), IUnitOfWork
{
    // Administration Repositories
    private IAuditLogRepository _auditLogRepository;
    private ICatalogRepository _catalogRepository;
    private IParameterRepository _parameterRepository;
    private IFileRepository _fileRepository;
    private INotificationPushRepository _notificationPushRepository;
    private INotificationPushUserRepository _notificationPushUserRepository;
    private ICountryRepository _countryRepository;
    private IProvinceRepository _provinceRepository;
    private IRegionRepository _regionRepository;

    // Authentication Repositories
    private IDeviceRepository _deviceRepository;
    private IUserRepository _userRepository;
    private IUserDeviceRepository _userDeviceRepository;
    private IPersonRepository _personRepository;
    private IUserRegistrationFormRepository _userRegistrationFormRepository;
    private IUserDevicePushTokenRepository _userDevicePushTokenRepository;

    // Core Repositories
    private IQueueMessageRepository _queueMessageRepository;

    // Gym Repositories
    private IGymRepository _gymRepository;
    private IMembershipTypeRepository _membershipTypeRepository;
    private IMembershipRepository _membershipRepository;
    private IGymPhotoRepository _gymPhotoRepository;
    private IGymVideoRepository _gymVideoRepository;
    private IGymMachineRepository _gymMachineRepository;
    private ITrainerRepository _trainerRepository;
    private IGroupClassRepository _groupClassRepository;
    private IClassScheduleRepository _classScheduleRepository;
    private IClassReservationRepository _classReservationRepository;
    private IGymReviewRepository _gymReviewRepository;
    private ITrainerRatingRepository _trainerRatingRepository;
    private IPersonalGoalRepository _personalGoalRepository;

    // Administration Repository Properties
    public IAuditLogRepository AuditLogRepository => _auditLogRepository ??=
        new AuditLogRepository(Context, LoggerFactory.CreateLogger<AuditLogRepository>());

    public ICatalogRepository CatalogRepository => _catalogRepository ??=
        new CatalogRepository(Context, LoggerFactory.CreateLogger<CatalogRepository>());

    public IParameterRepository ParameterRepository => _parameterRepository ??=
        new ParameterRepository(Context, LoggerFactory.CreateLogger<ParameterRepository>());

    public IFileRepository FileRepository => _fileRepository ??= 
        new FileRepository(Context, LoggerFactory.CreateLogger<FileRepository>());

    public INotificationPushRepository NotificationPushRepository => _notificationPushRepository ??=
        new NotificationPushRepository(Context, LoggerFactory.CreateLogger<NotificationPushRepository>());

    public INotificationPushUserRepository NotificationPushUserRepository => _notificationPushUserRepository ??=
        new NotificationPushUserRepository(Context, LoggerFactory.CreateLogger<NotificationPushUserRepository>());

    public ICountryRepository CountryRepository => _countryRepository ??=
        new CountryRepository(Context, LoggerFactory.CreateLogger<CountryRepository>());

    public IProvinceRepository ProvinceRepository => _provinceRepository ??=
        new ProvinceRepository(Context, LoggerFactory.CreateLogger<ProvinceRepository>());

    public IRegionRepository RegionRepository => _regionRepository ??=
        new RegionRepository(Context, LoggerFactory.CreateLogger<RegionRepository>());

    // Authentication Repository Properties
    public IDeviceRepository DeviceRepository => _deviceRepository ??= 
        new DeviceRepository(Context, LoggerFactory.CreateLogger<DeviceRepository>());

    public IUserRepository UserRepository => _userRepository ??= 
        new UserRepository(Context, LoggerFactory.CreateLogger<UserRepository>());

    public IUserDeviceRepository UserDeviceRepository => _userDeviceRepository ??= 
        new UserDeviceRepository(Context, LoggerFactory.CreateLogger<UserDeviceRepository>());

    public IPersonRepository PersonRepository => _personRepository ??= 
        new PersonRepository(Context, LoggerFactory.CreateLogger<PersonRepository>());

    public IUserRegistrationFormRepository UserRegistrationFormRepository => _userRegistrationFormRepository ??= 
        new UserRegistrationFormRepository(Context, LoggerFactory.CreateLogger<UserRegistrationFormRepository>());

    public IUserDevicePushTokenRepository UserDevicePushTokenRepository => _userDevicePushTokenRepository ??= 
        new UserDevicePushTokenRepository(Context, LoggerFactory.CreateLogger<UserDevicePushTokenRepository>());

    // Core Repository Properties
    public IQueueMessageRepository QueueMessageRepository => _queueMessageRepository ??= 
        new QueueMessageRepository(Context, LoggerFactory.CreateLogger<QueueMessageRepository>());

    // Gym Repository Properties
    public IGymRepository GymRepository => _gymRepository ??=
        new GymRepository(Context, LoggerFactory.CreateLogger<GymRepository>());

    public IMembershipTypeRepository MembershipTypeRepository => _membershipTypeRepository ??=
        new MembershipTypeRepository(Context, LoggerFactory.CreateLogger<MembershipTypeRepository>());

    public IMembershipRepository MembershipRepository => _membershipRepository ??=
        new MembershipRepository(Context, LoggerFactory.CreateLogger<MembershipRepository>());

    public IGymPhotoRepository GymPhotoRepository => _gymPhotoRepository ??=
        new GymPhotoRepository(Context, LoggerFactory.CreateLogger<GymPhotoRepository>());

    public IGymVideoRepository GymVideoRepository => _gymVideoRepository ??=
        new GymVideoRepository(Context, LoggerFactory.CreateLogger<GymVideoRepository>());

    public IGymMachineRepository GymMachineRepository => _gymMachineRepository ??=
        new GymMachineRepository(Context, LoggerFactory.CreateLogger<GymMachineRepository>());

    public ITrainerRepository TrainerRepository => _trainerRepository ??=
        new TrainerRepository(Context, LoggerFactory.CreateLogger<TrainerRepository>());

    public IGroupClassRepository GroupClassRepository => _groupClassRepository ??=
        new GroupClassRepository(Context, LoggerFactory.CreateLogger<GroupClassRepository>());

    public IClassScheduleRepository ClassScheduleRepository => _classScheduleRepository ??=
        new ClassScheduleRepository(Context, LoggerFactory.CreateLogger<ClassScheduleRepository>());

    public IClassReservationRepository ClassReservationRepository => _classReservationRepository ??=
        new ClassReservationRepository(Context, LoggerFactory.CreateLogger<ClassReservationRepository>());

    public IGymReviewRepository GymReviewRepository => _gymReviewRepository ??=
        new GymReviewRepository(Context, LoggerFactory.CreateLogger<GymReviewRepository>());

    public ITrainerRatingRepository TrainerRatingRepository => _trainerRatingRepository ??=
        new TrainerRatingRepository(Context, LoggerFactory.CreateLogger<TrainerRatingRepository>());

    public IPersonalGoalRepository PersonalGoalRepository => _personalGoalRepository ??=
        new PersonalGoalRepository(Context, LoggerFactory.CreateLogger<PersonalGoalRepository>());
}
