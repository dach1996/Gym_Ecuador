using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class SeriesRecordRepository : GenericRepository<SeriesRecord>, ISeriesRecordRepository
{
    public SeriesRecordRepository(PersistenceContext dbContext, ILogger<SeriesRecordRepository> logger) : base(dbContext, logger)
    {
    }
}
