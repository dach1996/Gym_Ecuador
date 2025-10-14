using Microsoft.Extensions.Logging;

namespace Common.Card.Implementation;

/// <summary>
/// Clase base para implementación de servicios
/// </summary>
public abstract class CardServicesBase
{
    protected abstract CardImplementationName ImplementationName { get; }

    protected readonly ILogger<CardServicesBase> Logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    protected CardServicesBase(ILogger<CardServicesBase> logger)
    {
        Logger = logger;
    }
}