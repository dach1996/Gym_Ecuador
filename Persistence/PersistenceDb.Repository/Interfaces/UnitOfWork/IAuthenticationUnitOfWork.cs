using PersistenceDb.Repository.Interfaces.Authentication;
namespace PersistenceDb.Repository.Interfaces.UnitOfWork;
public interface IAuthenticationUnitOfWork : IUnitOfWork
{
    IDeviceRepository DeviceRepository { get; }
    IUserRepository UserRepository { get; }
    IUserDeviceRepository UserDeviceRepository { get; }
    ICardRepository CardRepository { get; }
    IPersonRepository PersonRepository { get; }
    IUserRegistrationFormRepository UserRegistrationFormRepository { get; }
    IUserDevicePushTokenRepository UserDevicePushTokenRepository { get; }
}