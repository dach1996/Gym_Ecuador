using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class ParameterRepository : GenericRepository<Parameter>, IParameterRepository
{
    public ParameterRepository(PersistenceContext dbContext, ILogger<ParameterRepository> logger) : base(dbContext, logger)
    {
    }
}