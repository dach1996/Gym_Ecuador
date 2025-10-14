using Autofac;
using PersistenceDb.Repository.Interfaces.UnitOfWork;
using PersistenceDb.UnitOfWork;
using Module = Autofac.Module;

namespace PersistenceDb.Infrastructure.Modules;

public class ManagerUnitOfWorkModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //UoW
        builder.RegisterType<UnitOfWorkManager>().As<IUnitOfWorkManager>().InstancePerLifetimeScope();
    }
}