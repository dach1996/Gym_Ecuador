using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class FileRepository : GenericRepository<FilePersistence>, IFileRepository
{
    public FileRepository(PersistenceContext dbContext, ILogger<FileRepository> logger) : base(dbContext, logger)
    {
    }
}