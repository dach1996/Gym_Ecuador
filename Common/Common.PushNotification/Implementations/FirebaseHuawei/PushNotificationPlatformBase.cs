using Common.PushNotification.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.PushNotification.Implementations.FirebaseHuawei;
/// <summary>
/// Clase base de push notificación
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="configuration"></param>
public abstract class PushNotificationPlatformBase(
    ILogger<PushNotificationPlatformBase> logger,
    IConfiguration configuration)
{
    protected abstract PushNotificationPlatformImplementationNames Implementation { get; }
    protected readonly ILogger<PushNotificationPlatformBase> Logger = logger;
    protected readonly IConfiguration Configuration = configuration;
    protected const string DISABLED_MESSAGE = "Implementación Desactivada";
}
