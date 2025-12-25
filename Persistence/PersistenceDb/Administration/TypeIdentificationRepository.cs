using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Administration;
using PersistenceDb.Repository.Interfaces.Administration;

namespace PersistenceDb.Administration;

public class TypeIdentificationRepository(PersistenceContext dbContext, ILogger<TypeIdentificationRepository> logger) 
    : GenericRepository<TypeIdentification>(dbContext, logger), ITypeIdentificationRepository
{
}

