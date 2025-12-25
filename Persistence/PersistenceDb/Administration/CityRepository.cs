using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CityRepository(PersistenceContext dbContext, ILogger<CityRepository> logger) 
    : GenericRepository<City>(dbContext, logger), ICityRepository
{
}

