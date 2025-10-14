using System.Runtime.Serialization;
namespace Common.WebCommon.Models.Enum;
/// <summary>
/// Códigos de Tópicos utilizados para Notificaciones
/// </summary>
public enum TopicType
{
    /// <summary>
    /// Tokens
    /// </summary>
    [EnumMember(Value = "Tokens")]
    Token,

    /// <summary>
    /// Tópico Android
    /// </summary>
    [EnumMember(Value = "Plataformas Android")]
    Android,

    /// <summary>
    /// Tópico iOs
    /// </summary>
    [EnumMember(Value = "Plataformas iOS")]
    Ios,
    /// <summary>
    /// Todos los dispositivos Registrados
    /// </summary>
    [EnumMember(Value = "Todos los Dispositivos")]
    Global,
}