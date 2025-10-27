using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class PersonalGoalRepository : GenericRepository<PersonalGoal>, IPersonalGoalRepository
{
    public PersonalGoalRepository(PersistenceContext dbContext, ILogger<PersonalGoalRepository> logger) : base(dbContext, logger)
    {
    }
}
