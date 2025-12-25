using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class ForumRepository(PersistenceContext dbContext, ILogger<ForumRepository> logger) 
    : GenericRepository<Forum>(dbContext, logger), IForumRepository
{
}

