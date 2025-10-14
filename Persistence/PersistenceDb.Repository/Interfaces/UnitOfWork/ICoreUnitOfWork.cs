using PersistenceDb.Repository.Interfaces.Core;
namespace PersistenceDb.Repository.Interfaces.UnitOfWork;
public interface ICoreUnitOfWork : IUnitOfWork
{
    ICompanionRepository CompanionRepository { get; }
    IReserveSeatRepository ReserveSeatRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderCancelationRepository OrderCancelationRepository { get; }
    IOrderSeatPersonRepository OrderSeatPersonRepository { get; }
    IOrderPaymentRepository OrderPaymentRepository { get; }
    IQueueMessageRepository QueueMessageRepository { get; }
    ICooperativeRouteRepository CooperativeRouteRepository { get; }
}
