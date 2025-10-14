using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PersistenceDb.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace PersistenceDb.UnitOfWork;

public class AuthenticationUnitOfWork : UnitOfWork, IAuthenticationUnitOfWork
{
    private IDeviceRepository _mobileDeviceRepository;
    private IUserRepository _userAppRepository;
    private IUserDeviceRepository _userDeviceRepository;
    private ICardRepository _cardRepository;
    private IPersonRepository _personRepository;
    private IUserRegistrationFormRepository _userRegistrationFormRepository;
    private IUserDevicePushTokenRepository _userDevicePushTokenRepository;

     public AuthenticationUnitOfWork(
        ILoggerFactory loggerFactory,
        IConfiguration configuration) : base(loggerFactory, configuration)
    {
    }

    public IDeviceRepository DeviceRepository => _mobileDeviceRepository ??= new DeviceRepository(Context,
        LoggerFactory.CreateLogger<DeviceRepository>());

    public IUserRepository UserRepository => _userAppRepository ??= new UserRepository(Context,
        LoggerFactory.CreateLogger<UserRepository>());

    public IUserDeviceRepository UserDeviceRepository => _userDeviceRepository ??= new UserDeviceRepository(Context,
        LoggerFactory.CreateLogger<UserDeviceRepository>());

    public ICardRepository CardRepository => _cardRepository ??= new CardRepository(Context,
        LoggerFactory.CreateLogger<CardRepository>());

    public IPersonRepository PersonRepository => _personRepository ??= new PersonRepository(Context,
        LoggerFactory.CreateLogger<PersonRepository>());

    public IUserRegistrationFormRepository UserRegistrationFormRepository => _userRegistrationFormRepository ??= new UserRegistrationFormRepository(Context,
        LoggerFactory.CreateLogger<UserRegistrationFormRepository>());

    public IUserDevicePushTokenRepository UserDevicePushTokenRepository => _userDevicePushTokenRepository ??= new UserDevicePushTokenRepository(Context,
        LoggerFactory.CreateLogger<UserDevicePushTokenRepository>());
}