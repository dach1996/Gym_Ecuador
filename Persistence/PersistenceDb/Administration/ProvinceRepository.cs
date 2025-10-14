using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class ProvinceRepository(PersistenceContext dbContext, ILogger<ProvinceRepository> logger) 
    : GenericRepository<Province>(dbContext, logger), IProvinceRepository
{
}