using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class FileBasePathRepository(PersistenceContext dbContext, ILogger<FileBasePathRepository> logger) 
    : GenericRepository<FileBasePath>(dbContext, logger), IFileBasePathRepository
{
}

