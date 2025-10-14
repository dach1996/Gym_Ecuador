using Common.Queue.Model.Enum;

namespace Common.Queue.Model.Template;
/// <summary>
/// Modelo queue para expirar asiento
/// </summary>
public class ExpiredSeatQueueTemplate : IQueueTemplate
{
    /// <summary>
    /// Queue Name
    /// </summary>
    public QueueTemplateName QueueTemplateName => QueueTemplateName.ExpiredSeat;

    /// <summary>
    /// Identificador Externo
    /// </summary>
    /// <value></value>
    public Guid InternalIdentifier { get; set; }

    /// <summary>
    /// RequestId
    /// </summary>
    /// <value></value>
    public string RequestId { get; set; }

    /// <summary>
    /// Ids de asientos a Expirar
    /// </summary>
    /// <value></value>
    public IEnumerable<int> SeatIds { get; set; }

    /// <summary>
    /// Enviar notificación push
    /// </summary>
    /// <value></value>
    public bool SendPushNotification { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="seatIds"></param>
    /// <param name="sendPushNotification"></param>
    public ExpiredSeatQueueTemplate(IEnumerable<int> seatIds, bool sendPushNotification = true)
    {
        SeatIds = seatIds;
        SendPushNotification = sendPushNotification;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="seatIds"></param>
    public ExpiredSeatQueueTemplate(int seatIds, bool sendPushNotification = true)
     : this([seatIds], sendPushNotification) { }

    /// <summary>
    /// Constructor
    /// </summary>
    public ExpiredSeatQueueTemplate()
    {

    }
}
