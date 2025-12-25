using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class ForumCommentRepository(PersistenceContext dbContext, ILogger<ForumCommentRepository> logger) 
    : GenericRepository<ForumComment>(dbContext, logger), IForumCommentRepository
{
}

