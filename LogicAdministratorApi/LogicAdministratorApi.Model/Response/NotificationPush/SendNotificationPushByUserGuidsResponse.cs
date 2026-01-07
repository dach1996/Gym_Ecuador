using LogicCommon.Model.Response.NotificationPush;

namespace LogicAdministratorApi.Model.Response.NotificationPush;

/// <summary>
/// Respuesta de envío de notificación push por UserGuids
/// </summary>
public class SendNotificationPushByUserGuidsResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Diccionario de Notificaciones Push por UserGuid
    /// </summary>
    public Dictionary<Guid, NotificationPushUserGuidResponse> NotificationPushUsers { get; set; }

    /// <summary>
    /// Total de usuarios a los que se intentó enviar
    /// </summary>
    public int TotalUsers { get; set; }

    /// <summary>
    /// Total de usuarios con éxito
    /// </summary>
    public int SuccessUsers { get; set; }

    /// <summary>
    /// Total de usuarios con error
    /// </summary>
    public int FailUsers { get; set; }
}

/// <summary>
/// Respuesta de Notificación Push por UserGuid
/// </summary>
public class NotificationPushUserGuidResponse
{
    /// <summary>
    /// Total de dispositivos registrados
    /// </summary>
    public int TotalDevices { get; set; }

    /// <summary>
    /// Dispositivos con notificación enviada exitosamente
    /// </summary>
    public int SuccessDevices { get; set; }

    /// <summary>
    /// Dispositivos con error
    /// </summary>
    public int FailDevices { get; set; }

    /// <summary>
    /// Operación exitosa?
    /// </summary>
    public bool SuccessOperation { get; set; }
}

