using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class UserRegistrationFormRepository : GenericRepository<UserRegistrationForm>, IUserRegistrationFormRepository
{
    public UserRegistrationFormRepository(PersistenceContext dbContext, ILogger<UserRegistrationFormRepository> logger) : base(dbContext, logger)
    {
    }
}