using Common.Utils.Cryptography.Argon2;

namespace LogicAdministratorApi.BusinessLogic.UserAdministratorHandler;

/// <summary>
/// Clase base para handlers de usuario administrador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class UserAdministratorBase<TRequest, TResponse>(
    ILogger<UserAdministratorBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene la contraseña encriptada
    /// </summary>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    /// <returns></returns>
    protected static string GetPasswordEncrypted(string password, string salt) => Argon2.GenerateHash(password, salt);
}

