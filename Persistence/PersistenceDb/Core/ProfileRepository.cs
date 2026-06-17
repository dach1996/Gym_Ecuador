using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ProfileRepository(PersistenceContext dbContext, ILogger<ProfileRepository> logger) : GenericRepository<Profile>(dbContext, logger), IProfileRepository;
