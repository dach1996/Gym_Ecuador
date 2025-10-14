using Common.PushNotification.Model;
using Microsoft.Extensions.Logging;

namespace Common.PushNotification.Implementations;
/// <summary>
/// Clase base de push notificación
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public abstract class PushNotificationBase(
    ILogger<PushNotificationBase> logger)
{
    protected abstract PushNotificationImplementationNames Implementation { get; }
    protected readonly ILogger<PushNotificationBase> Logger = logger;
}
