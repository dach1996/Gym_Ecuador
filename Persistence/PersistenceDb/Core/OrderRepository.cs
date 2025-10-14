using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(PersistenceContext dbContext, ILogger<OrderRepository> logger) : base(dbContext, logger)
    {
    }
}