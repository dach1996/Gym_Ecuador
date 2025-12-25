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
        builder.RegisterType<CatalogLanguageRepository>().As<ICatalogLanguageRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ParameterRepository>().As<IParameterRepository>().InstancePerLifetimeScope();
        builder.RegisterType<FileRepository>().As<IFileRepository>().InstancePerLifetimeScope();
        builder.RegisterType<NotificationPushRepository>().As<INotificationPushRepository>().InstancePerLifetimeScope();
        builder.RegisterType<NotificationPushUserRepository>().As<INotificationPushUserRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CountryRepository>().As<ICountryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ProvinceRepository>().As<IProvinceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<RegionRepository>().As<IRegionRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ParishRepository>().As<IParishRepository>().InstancePerLifetimeScope();
        builder.RegisterType<TypeIdentificationRepository>().As<ITypeIdentificationRepository>().InstancePerLifetimeScope();
        //UoW
        builder.RegisterType<MainUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
    }
}