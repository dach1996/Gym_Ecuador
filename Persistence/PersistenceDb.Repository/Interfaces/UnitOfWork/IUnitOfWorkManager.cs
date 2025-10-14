namespace PersistenceDb.Repository.Interfaces.UnitOfWork;

public interface IUnitOfWorkManager
{
    IAdministrationUnitOfWork GetNewAdministrationUnitOfWork();
    IAuthenticationUnitOfWork GetNewAuthenticationUnitOfWork();
    ICoreUnitOfWork GetNewCoreUnitOfWork();
}