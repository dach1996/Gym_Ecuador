using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ClassReservationRepository : GenericRepository<ClassReservation>, IClassReservationRepository
{
    public ClassReservationRepository(PersistenceContext dbContext, ILogger<ClassReservationRepository> logger) : base(dbContext, logger)
    {
    }
}
