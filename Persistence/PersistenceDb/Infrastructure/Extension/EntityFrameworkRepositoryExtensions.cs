using Autofac;
using PersistenceDb.Infrastructure.Modules;

namespace PersistenceDb.Infrastructure.Extension;

public static class EntityFrameworkRepositoryExtensions
{
    public static void UseUnitOfWorkRepository(this ContainerBuilder builder)
    {
        builder.RegisterModule<AdministrationModule>();
        builder.RegisterModule<AuthenticationModule>();
        builder.RegisterModule<ManagerUnitOfWorkModule>();
        builder.RegisterModule<CoreModule>();
    }
}