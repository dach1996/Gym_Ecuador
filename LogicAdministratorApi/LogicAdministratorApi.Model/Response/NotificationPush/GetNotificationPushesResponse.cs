using Common.WebApi.Models;

namespace LogicAdministratorApi.Model.Response.NotificationPush;

/// <summary>
/// Respuesta de obtener notificaciones push paginadas
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="registers"></param>
public class GetNotificationPushesResponse(int totalRegister, IEnumerable<NotificationPushItem> registers) : IPaginatorApiResponse<NotificationPushItem>
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
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; } = totalRegister;

    /// <summary>
    /// Registros
    /// </summary>
    public IEnumerable<NotificationPushItem> Registers { get; set; } = registers;
}

/// <summary>
/// Item de notificación push
/// </summary>
public class NotificationPushItem
{
    /// <summary>
    /// Identificador de la notificación push
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título de la notificación
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Descripción de la notificación
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    public DateTime RegisterDate { get; set; }

    /// <summary>
    /// Tipo de notificación push
    /// </summary>
    public string PushNotificationType { get; set; }

    /// <summary>
    /// Valor del tipo de notificación (Tópico o Token)
    /// </summary>
    public string PushNotificationValue { get; set; }

    /// <summary>
    /// Indica si permite ver al usuario
    /// </summary>
    public bool AllowViewUser { get; set; }

    /// <summary>
    /// Cantidad total de usuarios a los que se envió
    /// </summary>
    public int TotalUsersSent { get; set; }

    /// <summary>
    /// Cantidad de usuarios que recibieron exitosamente la notificación
    /// </summary>
    public int SuccessfullySentCount { get; set; }

    /// <summary>
    /// Cantidad de usuarios con error en el envío
    /// </summary>
    public int ErrorCount { get; set; }
}

