using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CountryRepository(PersistenceContext dbContext, ILogger<CountryRepository> logger) 
    : GenericRepository<Country>(dbContext, logger), ICountryRepository
{
}