using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class QueueMessageRepository(PersistenceContext dbContext, ILogger<QueueMessageRepository> logger)
    : GenericRepository<QueueMessage>(dbContext, logger), IQueueMessageRepository
{
}