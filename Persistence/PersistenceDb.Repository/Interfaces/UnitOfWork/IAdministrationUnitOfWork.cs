using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Repository.Interfaces.UnitOfWork;
public interface IAdministrationUnitOfWork : IUnitOfWork
{

    IAuditLogRepository AuditLogRepository { get; }
    ICatalogRepository CatalogRepository { get; }
    IParameterRepository ParameterRepository { get; }
    IPlaceRepository PlaceRepository { get; }
    IPlaceUserRepository PlaceUserRepository { get; }
    ICooperativeRepository CooperativeRepository { get; }
    IFileRepository FileRepository { get; }
    ICooperativeBusServiceRepository CooperativeBusServiceRepository { get; }
    ICooperativeBusRepository CooperativeBusRepository { get; }
    IServiceRepository ServiceRepository { get; }
    IStationRepository StationRepository { get; }
    INotificationPushRepository NotificationPushRepository { get; }
    INotificationPushUserRepository NotificationPushUserRepository { get; }
    ICountryRepository CountryRepository { get; }
    IProvinceRepository ProvinceRepository { get; }
    IRegionRepository RegionRepository { get; }
    ITransportPointRepository TransportPointRepository { get; }
    ICooperativeTransportPointRepository CooperativeTransportPointRepository { get; }
}