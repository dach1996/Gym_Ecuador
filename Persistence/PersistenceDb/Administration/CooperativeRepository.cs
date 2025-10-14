using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class CooperativeRepository : GenericRepository<Cooperative>, ICooperativeRepository
{
    public CooperativeRepository(PersistenceContext dbContext, ILogger<CooperativeRepository> logger) : base(dbContext, logger)
    {
    }
}