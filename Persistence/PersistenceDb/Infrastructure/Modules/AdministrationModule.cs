using Autofac;
using PersistenceDb.Administration;
using PersistenceDb.Repository.Interfaces.Administration;
using PersistenceDb.Repository.Interfaces.UnitOfWork;
using PersistenceDb.UnitOfWork;
using Module = Autofac.Module;

namespace PersistenceDb.Infrastructure.Modules;

public class AdministrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AuditLogRepository>().As<IAuditLogRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CatalogRepository>().As<ICatalogRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ParameterRepository>().As<IParameterRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CooperativeRepository>().As<ICooperativeRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PlaceRepository>().As<IPlaceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PlaceUserRepository>().As<IPlaceUserRepository>().InstancePerLifetimeScope();
        builder.RegisterType<FileRepository>().As<IFileRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CooperativeBusRepository>().As<ICooperativeBusRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CooperativeBusServiceRepository>().As<ICooperativeBusServiceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ServiceRepository>().As<IServiceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<StationRepository>().As<IStationRepository>().InstancePerLifetimeScope();
        builder.RegisterType<NotificationPushRepository>().As<INotificationPushRepository>().InstancePerLifetimeScope();
        builder.RegisterType<NotificationPushUserRepository>().As<INotificationPushUserRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ProvinceRepository>().As<IProvinceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<RegionRepository>().As<IRegionRepository>().InstancePerLifetimeScope();
        builder.RegisterType<TransportPointRepository>().As<ITransportPointRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CooperativeTransportPointRepository>().As<ICooperativeTransportPointRepository>().InstancePerLifetimeScope();
        //UoW
        builder.RegisterType<AdministrationUnitOfWork>().As<IAdministrationUnitOfWork>().InstancePerLifetimeScope();
    }
}