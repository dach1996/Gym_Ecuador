using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class OrderCancelationRepository : GenericRepository<OrderCancelation>, IOrderCancelationRepository
{
    public OrderCancelationRepository(PersistenceContext dbContext, ILogger<OrderCancelationRepository> logger) : base(dbContext, logger)
    {
    }
}