namespace LogicCommon.Model.Response.NotificationPush;
/// <summary>
/// Respuesta de Envío de Notificación Push
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="totalRegister"></param>
/// <param name="successCount"></param>
public class NotificationPushResponse(Dictionary<int, NotificationPushResponse.NotificationPushUserResponse> notificationPushUsers)
{
    /// <summary>
    /// Diccionario de Notificaciones Push por usuario
    /// </summary>
    /// <value></value>
    public Dictionary<int, NotificationPushUserResponse> NotificationPushUsers { get; set; } = notificationPushUsers;

    /// <summary>
    /// Respuesta de Notificación Push por usuario
    /// </summary>
    public class NotificationPushUserResponse(int totalRegister, int successCount)
    {
        /// <summary>
        /// Total Registros a Enviar
        /// </summary>
        /// <value></value>
        public int TotalRegister { get; set; } = totalRegister;

        /// <summary>
        /// Registros enviados con éxito
        /// </summary>
        /// <value></value>
        public int SuccessCount { get; set; } = successCount;

        /// <summary>
        /// Registros con Error
        /// </summary>
        /// <value></value>
        public int FailCount { get => TotalRegister - SuccessCount; }

        /// <summary>
        /// Operación Exitosa?
        /// </summary>
        /// <value></value>
        public bool SuccessOperation { get => TotalRegister == SuccessCount; }
    }
}