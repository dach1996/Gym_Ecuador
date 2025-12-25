using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class ArticleRepository(PersistenceContext dbContext, ILogger<ArticleRepository> logger) 
    : GenericRepository<Article>(dbContext, logger), IArticleRepository
{
}

