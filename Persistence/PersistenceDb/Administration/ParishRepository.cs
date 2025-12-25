using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class ParishRepository(PersistenceContext dbContext, ILogger<ParishRepository> logger) 
    : GenericRepository<Parish>(dbContext, logger), IParishRepository
{
}

