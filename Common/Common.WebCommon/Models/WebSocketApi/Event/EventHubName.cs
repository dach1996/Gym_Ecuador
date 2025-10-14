using System.Runtime.Serialization;

namespace Common.WebCommon.Models.WebSocketApi.Event;
/// <summary>
/// Nombre de eventos para Hub
/// </summary>
public enum EventHubName
{
    /// <summary>
    /// Actualiza la asiento
    /// </summary>
    [EnumMember(Value = nameof(UpdateSeatEvent))]
    UpdateSeatEvent,

    /// <summary>
    /// Notifica un evento por grupo
    /// </summary>    /// 
    [EnumMember(Value = nameof(GetUserNumberByGroup))]
    GetUserNumberByGroup,

    /// <summary>
    /// Actualiza el número de usuarios viendo un ticket
    /// </summary>
    [EnumMember(Value = nameof(UpdateUsersVieweingTicket))]
    UpdateUsersVieweingTicket,

}