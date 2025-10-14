using Autofac;
using PersistenceDb.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;
using PersistenceDb.Repository.Interfaces.UnitOfWork;
using PersistenceDb.UnitOfWork;

namespace PersistenceDb.Infrastructure.Modules;

public class AuthenticationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DeviceRepository>().As<IDeviceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserDeviceRepository>().As<IUserDeviceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CardRepository>().As<ICardRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRegistrationFormRepository>().As<IUserRegistrationFormRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserDevicePushTokenRepository>().As<IUserDevicePushTokenRepository>().InstancePerLifetimeScope();
        //UoW
        builder.RegisterType<AuthenticationUnitOfWork>().As<IAuthenticationUnitOfWork>().InstancePerLifetimeScope();
    }
}