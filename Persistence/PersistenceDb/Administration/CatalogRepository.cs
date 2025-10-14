using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CatalogRepository(PersistenceContext dbContext, ILogger<CatalogRepository> logger) 
    : GenericRepository<Catalog>(dbContext, logger), ICatalogRepository
{
}