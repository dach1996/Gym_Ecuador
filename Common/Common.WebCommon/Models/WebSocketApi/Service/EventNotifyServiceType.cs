using System.Runtime.Serialization;

namespace Common.WebCommon.Models.WebSocketApi.Service;
/// <summary>
/// Servicios para notificador de eventos
/// </summary>
public enum EventNotifyServiceType
{
    [EnumMember(Value = nameof(UpdateSeat))]
    UpdateSeat
}