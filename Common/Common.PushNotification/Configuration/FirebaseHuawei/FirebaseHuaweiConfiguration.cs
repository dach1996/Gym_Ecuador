using Common.PushNotification.Model;

namespace Common.PushNotification.Configuration.FirebaseHuawei;
/// <summary>
/// Configuración para Firebase y Huawei
/// </summary>
public class FirebaseHuaweiConfiguration
{
    /// <summary>
    /// Implementaciones
    /// </summary>
    /// <value></value>
    public IEnumerable<PlatformNotificationPushImplementation> Implementations { get; set; }
}

/// <summary>
/// Clase de configuraciòn para Implementaciones Push
/// </summary>
public class PlatformNotificationPushImplementation
{
    /// <summary>
    /// Identificador
    /// </summary>
    /// <value></value>
    public PushNotificationPlatformImplementationNames Identifier { get; set; }

    /// <summary>
    /// Habilidato?
    /// </summary>
    /// <value></value>
    public bool Enable { get; set; }

    /// <summary>
    /// Credenciales en Base 64
    /// </summary>
    /// <value></value>
    public string CredentialBase64 { get; set; }
}