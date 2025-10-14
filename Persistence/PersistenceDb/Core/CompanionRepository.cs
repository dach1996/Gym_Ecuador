using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class CompanionRepository : GenericRepository<Companion>, ICompanionRepository
{
    public CompanionRepository(PersistenceContext dbContext, ILogger<CompanionRepository> logger) : base(dbContext, logger)
    {
    }
}