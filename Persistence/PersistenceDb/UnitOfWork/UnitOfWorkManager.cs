using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace PersistenceDb.UnitOfWork;

public class UnitOfWorkManager : IUnitOfWorkManager
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly IConfiguration _configuration;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="loggerFactory"></param>
    public UnitOfWorkManager(ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        _loggerFactory = loggerFactory;
        _configuration = configuration;
    }

    public IAdministrationUnitOfWork GetNewAdministrationUnitOfWork()
        => new AdministrationUnitOfWork(_loggerFactory, _configuration);

    public IAuthenticationUnitOfWork GetNewAuthenticationUnitOfWork()
        => new AuthenticationUnitOfWork(_loggerFactory, _configuration);

    public ICoreUnitOfWork GetNewCoreUnitOfWork()
        => new CoreUnitOfWork(_loggerFactory, _configuration);
}
