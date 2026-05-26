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
    private INotificationPushUserDeviceRepository _notificationPushUserDeviceRepository;
    private ICountryRepository _countryRepository;
    private IProvinceRepository _provinceRepository;
    private IRegionRepository _regionRepository;
    private ICityRepository _cityRepository;
    private IParishRepository _parishRepository;
    private ITypeIdentificationRepository _typeIdentificationRepository;
    private IArticleRepository _articleRepository;
    private IForumRepository _forumRepository;
    private IForumCommentRepository _forumCommentRepository;
    private IFileBasePathRepository _fileBasePathRepository;
    private IUserRoleScopeRepository _userRoleScopeRepository;
    private IRoleRepository _roleRepository;
    private IPlatformRepository _platformRepository;
    private IFunctionalityRepository _functionalityRepository;
    private IFunctionRepository _functionRepository;
    private IModuleRepository _moduleRepository;
    private IRoleFunctionalityRepository _roleFunctionalityRepository;
    // Authentication Repositories
    private IDeviceRepository _deviceRepository;
    private IUserRepository _userRepository;
    private IUserDeviceRepository _userDeviceRepository;
    private IPersonRepository _personRepository;
    private IUserRegistrationFormRepository _userRegistrationFormRepository;
    private IUserDevicePushTokenRepository _userDevicePushTokenRepository;

    // Core Repositories
    private IQueueMessageRepository _queueMessageRepository;
    private IProcessTrackingRepository _processTrackingRepository;
    private IProcessTrackingImageRepository _processTrackingImageRepository;
    private IPhysicalParameterRepository _physicalParameterRepository;
    private IProcessTrackingMeasurementRepository _processTrackingMeasurementRepository;
    private IRoutineRepository _routineRepository;
    private IExerciseRepository _exerciseRepository;
    private IExerciseTagRepository _exerciseTagRepository;
    private IRoutineExerciseRepository _routineExerciseRepository;
    private ISeriesRecordRepository _seriesRecordRepository;

    // Gym Repositories
    private IGymRepository _gymRepository;
    private IGymBranchRepository _gymBranchRepository;
    private IMembershipTypeRepository _membershipTypeRepository;
    private IMembershipRepository _membershipRepository;
    private IGymPhotoRepository _gymPhotoRepository;
    private IGymVideoRepository _gymVideoRepository;
    private IGymMachineRepository _gymMachineRepository;
    private ITrainerRepository _trainerRepository;
    private ITrainerGymRepository _trainerGymRepository;
    private IGroupClassRepository _groupClassRepository;
    private IClassScheduleRepository _classScheduleRepository;
    private IClassReservationRepository _classReservationRepository;
    private IGymReviewRepository _gymReviewRepository;
    private ITrainerRatingRepository _trainerRatingRepository;
    private IPersonalGoalRepository _personalGoalRepository;
    private IBranchPlanRepository _branchPlanRepository;
    private IClientGymBranchRepository _clientGymBranchRepository;
    private IClientMembershipRepository _clientMembershipRepository;
    private IServiceRepository _serviceRepository;
    private IGymBranchScheduleRepository _gymBranchScheduleRepository;
    private IGymBranchImageRepository _gymBranchImageRepository;
    private IEquipmentRepository _equipmentRepository;
    private IEquipmentImageRepository _equipmentImageRepository;
    private IPlanFeatureRepository _planFeatureRepository;

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

    public INotificationPushUserDeviceRepository NotificationPushUserDeviceRepository => _notificationPushUserDeviceRepository ??=
        new NotificationPushUserDeviceRepository(Context, LoggerFactory.CreateLogger<NotificationPushUserDeviceRepository>());

    public ICountryRepository CountryRepository => _countryRepository ??=
        new CountryRepository(Context, LoggerFactory.CreateLogger<CountryRepository>());

    public IProvinceRepository ProvinceRepository => _provinceRepository ??=
        new ProvinceRepository(Context, LoggerFactory.CreateLogger<ProvinceRepository>());

    public IRegionRepository RegionRepository => _regionRepository ??=
        new RegionRepository(Context, LoggerFactory.CreateLogger<RegionRepository>());

    public ICityRepository CityRepository => _cityRepository ??=
        new CityRepository(Context, LoggerFactory.CreateLogger<CityRepository>());

    public IParishRepository ParishRepository => _parishRepository ??=
        new ParishRepository(Context, LoggerFactory.CreateLogger<ParishRepository>());

    public ITypeIdentificationRepository TypeIdentificationRepository => _typeIdentificationRepository ??=
        new TypeIdentificationRepository(Context, LoggerFactory.CreateLogger<TypeIdentificationRepository>());

    public IArticleRepository ArticleRepository => _articleRepository ??=
        new ArticleRepository(Context, LoggerFactory.CreateLogger<ArticleRepository>());

    public IForumRepository ForumRepository => _forumRepository ??=
        new ForumRepository(Context, LoggerFactory.CreateLogger<ForumRepository>());

    public IForumCommentRepository ForumCommentRepository => _forumCommentRepository ??=
        new ForumCommentRepository(Context, LoggerFactory.CreateLogger<ForumCommentRepository>());

    public IFileBasePathRepository FileBasePathRepository => _fileBasePathRepository ??=
        new FileBasePathRepository(Context, LoggerFactory.CreateLogger<FileBasePathRepository>());

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

    public IProcessTrackingRepository ProcessTrackingRepository => _processTrackingRepository ??= 
        new ProcessTrackingRepository(Context, LoggerFactory.CreateLogger<ProcessTrackingRepository>());

    public IProcessTrackingImageRepository ProcessTrackingImageRepository => _processTrackingImageRepository ??= 
        new ProcessTrackingImageRepository(Context, LoggerFactory.CreateLogger<ProcessTrackingImageRepository>());

    public IPhysicalParameterRepository PhysicalParameterRepository => _physicalParameterRepository ??=
        new PhysicalParameterRepository(Context, LoggerFactory.CreateLogger<PhysicalParameterRepository>());

    public IProcessTrackingMeasurementRepository ProcessTrackingMeasurementRepository => _processTrackingMeasurementRepository ??=
        new ProcessTrackingMeasurementRepository(Context, LoggerFactory.CreateLogger<ProcessTrackingMeasurementRepository>());

    public IRoutineRepository RoutineRepository => _routineRepository ??=
        new RoutineRepository(Context, LoggerFactory.CreateLogger<RoutineRepository>());

    public IExerciseRepository ExerciseRepository => _exerciseRepository ??=
        new ExerciseRepository(Context, LoggerFactory.CreateLogger<ExerciseRepository>());

    public IExerciseTagRepository ExerciseTagRepository => _exerciseTagRepository ??=
        new ExerciseTagRepository(Context, LoggerFactory.CreateLogger<ExerciseTagRepository>());

    public IRoutineExerciseRepository RoutineExerciseRepository => _routineExerciseRepository ??=
        new RoutineExerciseRepository(Context, LoggerFactory.CreateLogger<RoutineExerciseRepository>());

    public ISeriesRecordRepository SeriesRecordRepository => _seriesRecordRepository ??=
        new SeriesRecordRepository(Context, LoggerFactory.CreateLogger<SeriesRecordRepository>());

    // Gym Repository Properties
    public IGymRepository GymRepository => _gymRepository ??=
        new GymRepository(Context, LoggerFactory.CreateLogger<GymRepository>());

    public IGymBranchRepository GymBranchRepository => _gymBranchRepository ??=
        new GymBranchRepository(Context, LoggerFactory.CreateLogger<GymBranchRepository>());

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

    public ITrainerGymRepository TrainerGymRepository => _trainerGymRepository ??=
        new TrainerGymRepository(Context, LoggerFactory.CreateLogger<TrainerGymRepository>());

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

    public IBranchPlanRepository BranchPlanRepository => _branchPlanRepository ??=
        new BranchPlanRepository(Context, LoggerFactory.CreateLogger<BranchPlanRepository>());

    public IClientGymBranchRepository ClientGymBranchRepository => _clientGymBranchRepository ??=
        new ClientGymBranchRepository(Context, LoggerFactory.CreateLogger<ClientGymBranchRepository>());

    public IClientMembershipRepository ClientMembershipRepository => _clientMembershipRepository ??=
        new ClientMembershipRepository(Context, LoggerFactory.CreateLogger<ClientMembershipRepository>());

    public IServiceRepository ServiceRepository => _serviceRepository ??=
        new ServiceRepository(Context, LoggerFactory.CreateLogger<ServiceRepository>());

    public IGymBranchScheduleRepository GymBranchScheduleRepository => _gymBranchScheduleRepository ??=
        new GymBranchScheduleRepository(Context, LoggerFactory.CreateLogger<GymBranchScheduleRepository>());

    public IGymBranchImageRepository GymBranchImageRepository => _gymBranchImageRepository ??=
        new GymBranchImageRepository(Context, LoggerFactory.CreateLogger<GymBranchImageRepository>());

    public IEquipmentRepository EquipmentRepository => _equipmentRepository ??=
        new EquipmentRepository(Context, LoggerFactory.CreateLogger<EquipmentRepository>());

    public IEquipmentImageRepository EquipmentImageRepository => _equipmentImageRepository ??=
        new EquipmentImageRepository(Context, LoggerFactory.CreateLogger<EquipmentImageRepository>());

    public IUserRoleScopeRepository UserRoleScopeRepository => _userRoleScopeRepository ??=
        new UserRoleScopeRepository(Context, LoggerFactory.CreateLogger<UserRoleScopeRepository>());

    public IRoleRepository RoleRepository => _roleRepository ??=
        new RoleRepository(Context, LoggerFactory.CreateLogger<RoleRepository>());

    public IPlatformRepository PlatformRepository => _platformRepository ??=
        new PlatformRepository(Context, LoggerFactory.CreateLogger<PlatformRepository>());

    public IFunctionalityRepository FunctionalityRepository => _functionalityRepository ??=
        new FunctionalityRepository(Context, LoggerFactory.CreateLogger<FunctionalityRepository>());

    public IFunctionRepository FunctionRepository => _functionRepository ??=
        new FunctionRepository(Context, LoggerFactory.CreateLogger<FunctionRepository>());

    public IModuleRepository ModuleRepository => _moduleRepository ??=
        new ModuleRepository(Context, LoggerFactory.CreateLogger<ModuleRepository>());

    public IRoleFunctionalityRepository RoleFunctionalityRepository => _roleFunctionalityRepository ??=
        new RoleFunctionalityRepository(Context, LoggerFactory.CreateLogger<RoleFunctionalityRepository>());

    public IPlanFeatureRepository PlanFeatureRepository => _planFeatureRepository ??=
        new PlanFeatureRepository(Context, LoggerFactory.CreateLogger<PlanFeatureRepository>());

}
