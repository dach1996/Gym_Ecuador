using Autofac;
using PersistenceDb.Core;
using PersistenceDb.Repository.Interfaces.Core;
using PersistenceDb.Repository.Interfaces.UnitOfWork;
using PersistenceDb.UnitOfWork;

namespace PersistenceDb.Infrastructure.Modules;

public class CoreModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<CompanionRepository>().As<ICompanionRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ReserveSeatRepository>().As<IReserveSeatRepository>().InstancePerLifetimeScope();
        builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
        builder.RegisterType<OrderSeatPersonRepository>().As<IOrderSeatPersonRepository>().InstancePerLifetimeScope();
        builder.RegisterType<OrderPaymentRepository>().As<IOrderPaymentRepository>().InstancePerLifetimeScope();
        builder.RegisterType<OrderCancelationRepository>().As<IOrderCancelationRepository>().InstancePerLifetimeScope();
        builder.RegisterType<QueueMessageRepository>().As<IQueueMessageRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CooperativeRouteRepository>().As<ICooperativeRouteRepository>().InstancePerLifetimeScope();
        //UoW
        builder.RegisterType<CoreUnitOfWork>().As<ICoreUnitOfWork>().InstancePerLifetimeScope();
    }
}