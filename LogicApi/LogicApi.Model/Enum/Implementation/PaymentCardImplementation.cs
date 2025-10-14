using System.Runtime.Serialization;

namespace LogicApi.Model.Enum.Implementation;
/// <summary>
/// Implementaciones para pago con tarjeta
/// </summary>
public enum PaymentCardImplementation
{
    /// <summary>
    /// Payphone
    /// </summary>
    [EnumMember(Value = "Payphone")]
    Payphone = 1
}