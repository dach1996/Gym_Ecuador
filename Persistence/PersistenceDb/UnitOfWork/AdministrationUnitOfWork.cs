using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersistenceDb.Administration;
using PersistenceDb.Repository.Interfaces.Administration;
using PersistenceDb.Repository.Interfaces.UnitOfWork;
namespace PersistenceDb.UnitOfWork;

public class AdministrationUnitOfWork(
 ILoggerFactory loggerFactory,
 IConfiguration configuration) : UnitOfWork(loggerFactory, configuration), IAdministrationUnitOfWork
{
    private IAuditLogRepository _auditLogRepository;
    private ICatalogRepository _catalogRepository;
    private IParameterRepository _parameterRepository;
    private IPlaceRepository _placeRepository;
    private IPlaceUserRepository _placeUserRepository;
    private ICooperativeRepository _cooperativeRepository;
    private IFileRepository _fileRepository;
    private ICooperativeBusRepository _cooperativeBusRepository;
    private ICooperativeBusServiceRepository _cooperativeBusServiceRepository;
    private IServiceRepository _serviceRepository;
    private IStationRepository _stationRepository;
    private INotificationPushRepository _notificationPushRepository;
    private INotificationPushUserRepository _notificationPushUserRepository;
    private ICountryRepository _countryRepository;
    private IProvinceRepository _provinceRepository;
    private IRegionRepository _regionRepository;
    private ITransportPointRepository _transportPointRepository;
    private ICooperativeTransportPointRepository _cooperativeTransportPointRepository;
    public IAuditLogRepository AuditLogRepository => _auditLogRepository ??=
        new AuditLogRepository(Context, LoggerFactory.CreateLogger<AuditLogRepository>());

    public ICatalogRepository CatalogRepository => _catalogRepository ??=
        new CatalogRepository(Context, LoggerFactory.CreateLogger<CatalogRepository>());

    public IParameterRepository ParameterRepository => _parameterRepository ??=
        new ParameterRepository(Context, LoggerFactory.CreateLogger<ParameterRepository>());

    public IPlaceRepository PlaceRepository => _placeRepository ??=
        new PlaceRepository(Context, LoggerFactory.CreateLogger<PlaceRepository>());

    public IPlaceUserRepository PlaceUserRepository => _placeUserRepository ??=
        new PlaceUserRepository(Context, LoggerFactory.CreateLogger<PlaceUserRepository>());

    public ICooperativeRepository CooperativeRepository => _cooperativeRepository ??=
     new CooperativeRepository(Context, LoggerFactory.CreateLogger<CooperativeRepository>());

    public IFileRepository FileRepository => _fileRepository ??= new FileRepository(Context,
        LoggerFactory.CreateLogger<FileRepository>());

    public ICooperativeBusRepository CooperativeBusRepository => _cooperativeBusRepository ??=
        new CooperativeBusRepository(Context, LoggerFactory.CreateLogger<CooperativeBusRepository>());

    public ICooperativeBusServiceRepository CooperativeBusServiceRepository => _cooperativeBusServiceRepository ??=
        new CooperativeBusServiceRepository(Context, LoggerFactory.CreateLogger<CooperativeBusServiceRepository>());

    public IServiceRepository ServiceRepository => _serviceRepository ??=
        new ServiceRepository(Context, LoggerFactory.CreateLogger<ServiceRepository>());

    public IStationRepository StationRepository => _stationRepository ??=
        new StationRepository(Context, LoggerFactory.CreateLogger<StationRepository>());

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

    public ITransportPointRepository TransportPointRepository => _transportPointRepository ??=
        new TransportPointRepository(Context, LoggerFactory.CreateLogger<TransportPointRepository>());

    public ICooperativeTransportPointRepository CooperativeTransportPointRepository => _cooperativeTransportPointRepository ??= 
        new CooperativeTransportPointRepository(Context, LoggerFactory.CreateLogger<CooperativeTransportPointRepository>());
}