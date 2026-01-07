using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.NotificationPush;

namespace LogicAdministratorApi.Model.Request.NotificationPush;

/// <summary>
/// Solicitud para enviar notificación push por UserGuids
/// </summary>
public class SendNotificationPushByUserGuidsRequest : IApiBaseRequest<SendNotificationPushByUserGuidsResponse>
{
    /// <summary>
    /// Título de la notificación
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    /// <summary>
    /// Cuerpo de la notificación
    /// </summary>
    [Required]
    [StringLength(1000)]
    public string Body { get; set; }

    /// <summary>
    /// URL de imagen de la notificación (opcional)
    /// </summary>
    [StringLength(500)]
    public string ImageUrl { get; set; }


    /// <summary>
    /// Lista de UserGuids a los que se enviará la notificación
    /// </summary>
    [Required]
    public IEnumerable<Guid> UserGuids { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

