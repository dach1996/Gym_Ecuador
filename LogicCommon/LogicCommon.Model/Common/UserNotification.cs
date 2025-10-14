namespace LogicCommon.Model.Common;
/// <summary>
/// Notificaciones de Topico
/// </summary>
public class UserNotification
{
    /// <summary>
    /// Id de Dispositivo
    /// </summary>
    /// <value></value>
    public int DeviceId { get; set; }

    /// <summary>
    /// Id de Usuario
    /// </summary>
    /// <value></value>
    public int UserId { get; set; }

    /// <summary>
    /// Implementation
    /// </summary>
    /// <value></value>
    public string Implementation { get; set; }
}

/// <summary>
/// Usarios y Tokens
/// </summary>
public class UserTokenNotification : UserNotification
{
    /// <summary>
    /// Token
    /// </summary>
    /// <value></value>
    public string Token { get; set; }
}