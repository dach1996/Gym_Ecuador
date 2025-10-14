using Microsoft.Extensions.Logging;

namespace Common.EventHub.Implementation;

/// <summary>
/// Clase base para implementación de servicios
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
public abstract class EventHubBase(ILogger<EventHubBase> logger)
{
    protected abstract EventHubImplementationName ImplementationName { get; }
    protected readonly ILogger<EventHubBase> Logger = logger;
}