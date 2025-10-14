using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(PersistenceContext dbContext, ILogger<PersonRepository> logger) : base(dbContext, logger)
    {
    }
}