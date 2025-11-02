using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CatalogLanguageRepository(PersistenceContext dbContext, ILogger<CatalogLanguageRepository> logger) 
    : GenericRepository<CatalogLanguage>(dbContext, logger), ICatalogLanguageRepository
{
}

