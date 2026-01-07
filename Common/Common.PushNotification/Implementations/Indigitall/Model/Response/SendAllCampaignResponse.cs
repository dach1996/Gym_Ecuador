namespace Common.PushNotification.Implementations.Indigitall.Model.Response;
/// <summary>
/// Respuesta de envío de Notificación por Campaña
/// </summary>
public class SendAllCampaignResponse : IndigitallGenericResponse<SendAllCampaignData>
{
   
}

/// <summary>
/// Resultados Enviados
/// </summary>
public class SendAllCampaignData
{
    /// <summary>
    /// Id de Notificación
    /// </summary>
    /// <value></value>
    public long SendingId { get; set; }
}

