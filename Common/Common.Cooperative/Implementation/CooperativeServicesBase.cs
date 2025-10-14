using Microsoft.Extensions.Logging;

namespace Common.Cooperative.Implementation;
/// <summary>
/// Implementaciòn base
/// /// </summary>
public abstract class CooperativeServicesBase
{
    /// <summary>
    /// Implementación
    /// </summary>
    /// <returns></returns>
    protected abstract CooperativeImplementationName ImplementationName { get; }

    /// <summary>
    /// Logger
    /// </summary>
    protected readonly ILogger<CooperativeServicesBase> Logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    protected CooperativeServicesBase(ILogger<CooperativeServicesBase> logger)
    {
        Logger = logger;
    }
}