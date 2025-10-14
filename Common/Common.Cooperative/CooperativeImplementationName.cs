using System.Runtime.Serialization;

namespace Common.Cooperative;
/// <summary>
/// Nombres de implementaciones de Cooperativa
/// </summary>
public enum CooperativeImplementationName
{
    [EnumMember(Value ="PANAMERICANA")]
    Panamericana,

    [EnumMember(Value ="TRANS_EXPRESS")]
    TrasandinaExpress,

    [EnumMember(Value ="IMBABURA")]
    Imbabura,
}