using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class PhysicalParameterRepository : GenericRepository<PhysicalParameter>, IPhysicalParameterRepository
{
    public PhysicalParameterRepository(PersistenceContext dbContext, ILogger<PhysicalParameterRepository> logger) : base(dbContext, logger)
    {
    }
}
