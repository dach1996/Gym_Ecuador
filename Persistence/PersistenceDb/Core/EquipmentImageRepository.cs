using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class EquipmentImageRepository : GenericRepository<EquipmentImage>, IEquipmentImageRepository
{
    public EquipmentImageRepository(PersistenceContext dbContext, ILogger<EquipmentImageRepository> logger) : base(dbContext, logger)
    {
    }
}

