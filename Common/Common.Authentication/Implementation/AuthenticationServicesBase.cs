using Microsoft.Extensions.Logging;

namespace Common.Authentication.Implementation;

/// <summary>
/// Clase base para implementación de servicios
/// </summary>
public abstract class AuthenticationServicesBase
{
    protected abstract AuthenticationImplementationName ImplementationName { get; }

    protected readonly ILogger<AuthenticationServicesBase> Logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    protected AuthenticationServicesBase(ILogger<AuthenticationServicesBase> logger) => Logger = logger;
}