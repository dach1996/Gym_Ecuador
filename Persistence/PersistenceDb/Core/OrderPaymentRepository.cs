using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class OrderPaymentRepository : GenericRepository<OrderPayment>, IOrderPaymentRepository
{
    public OrderPaymentRepository(PersistenceContext dbContext, ILogger<OrderPaymentRepository> logger) : base(dbContext, logger)
    {
    }
}