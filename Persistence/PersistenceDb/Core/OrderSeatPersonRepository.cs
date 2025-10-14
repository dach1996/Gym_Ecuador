using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class OrderSeatPersonRepository : GenericRepository<OrderSeatPerson>, IOrderSeatPersonRepository
{
    public OrderSeatPersonRepository(PersistenceContext dbContext, ILogger<OrderSeatPersonRepository> logger)
        : base(dbContext, logger)
    {
    }
}