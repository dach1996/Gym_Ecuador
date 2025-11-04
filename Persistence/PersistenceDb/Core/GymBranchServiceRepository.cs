using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

/// <summary>
/// Repositorio de Servicios de Sucursal de Gimnasio
/// </summary>
public class GymBranchServiceRepository : GenericRepository<GymBranchService>, IGymBranchServiceRepository
{
    public GymBranchServiceRepository(PersistenceContext dbContext, ILogger<GymBranchServiceRepository> logger) : base(dbContext, logger)
    {
    }
}

