using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersistenceDb.Core;
using PersistenceDb.Repository.Interfaces.Core;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace PersistenceDb.UnitOfWork;

public class CoreUnitOfWork(
    ILoggerFactory loggerFactory,
    IConfiguration configuration) : UnitOfWork(loggerFactory, configuration), ICoreUnitOfWork
{
    private ICompanionRepository _companionRepository;
    private IReserveSeatRepository _reserveSeatRepository;
    private IOrderRepository _orderRepository;
    private IOrderSeatPersonRepository _orderSeatPersonRepository;
    private IOrderPaymentRepository _orderPaymentRepository;
    private IOrderCancelationRepository _orderCancelationRepository;
    private IQueueMessageRepository _queueMessageRepository;
    private ICooperativeRouteRepository _cooperativeRouteRepository;

    public ICompanionRepository CompanionRepository => _companionRepository ??= new CompanionRepository(Context,
        LoggerFactory.CreateLogger<CompanionRepository>());

    public IReserveSeatRepository ReserveSeatRepository => _reserveSeatRepository ??= new ReserveSeatRepository(Context,
        LoggerFactory.CreateLogger<ReserveSeatRepository>());

    public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(Context,
        LoggerFactory.CreateLogger<OrderRepository>());

    public IOrderSeatPersonRepository OrderSeatPersonRepository => _orderSeatPersonRepository ??= new OrderSeatPersonRepository(Context,
        LoggerFactory.CreateLogger<OrderSeatPersonRepository>());

    public IOrderPaymentRepository OrderPaymentRepository => _orderPaymentRepository ??= new OrderPaymentRepository(Context,
        LoggerFactory.CreateLogger<OrderPaymentRepository>());

    public IOrderCancelationRepository OrderCancelationRepository => _orderCancelationRepository ??= new OrderCancelationRepository(Context,
        LoggerFactory.CreateLogger<OrderCancelationRepository>());

    public IQueueMessageRepository QueueMessageRepository => _queueMessageRepository ??= new QueueMessageRepository(Context,
        LoggerFactory.CreateLogger<QueueMessageRepository>());

    public ICooperativeRouteRepository CooperativeRouteRepository => _cooperativeRouteRepository ??= new CooperativeRouteRepository(Context,
        LoggerFactory.CreateLogger<CooperativeRouteRepository>()); 
}