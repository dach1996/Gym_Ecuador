using Autofac;
using PersistenceDb.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Infrastructure.Modules;

public class AuthenticationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DeviceRepository>().As<IDeviceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserDeviceRepository>().As<IUserDeviceRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRegistrationFormRepository>().As<IUserRegistrationFormRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserDevicePushTokenRepository>().As<IUserDevicePushTokenRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ModuleRepository>().As<IModuleRepository>().InstancePerLifetimeScope();
        builder.RegisterType<FunctionRepository>().As<IFunctionRepository>().InstancePerLifetimeScope();
        builder.RegisterType<FunctionalityRepository>().As<IFunctionalityRepository>().InstancePerLifetimeScope();
        builder.RegisterType<PlatformRepository>().As<IPlatformRepository>().InstancePerLifetimeScope();
        builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
        builder.RegisterType<RoleFunctionalityRepository>().As<IRoleFunctionalityRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserRoleScopeRepository>().As<IUserRoleScopeRepository>().InstancePerLifetimeScope();
    }
}