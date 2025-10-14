using System.Runtime.Serialization;

namespace Common.Queue.Model.Enum;
/// <summary>
///  Nombres de Queues
/// </summary>
public enum QueueTemplateName : byte
{
    [EnumMember(Value = "newusermail")]
    NewUserMail = 1,

    [EnumMember(Value = "forgottenpasswordmail")]
    ForgottenPasswordMail = 2,

    [EnumMember(Value = "expiredseat")]
    ExpiredSeat = 3,

    [EnumMember(Value = "expiredorder")]
    ExpiredOrder = 4,

    [EnumMember(Value = "voidseatreservation")]
    VoidSeatReservation = 5,
}