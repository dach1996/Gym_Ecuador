
using System.Runtime.Serialization;

namespace PersistenceDb.Models.Enums;

/// <summary>
/// Plataforma de Rol
/// </summary>
public enum RolePlatformType 
{
    /// <summary>
    /// Android
    /// </summary>
    [EnumMember(Value = "MOBILE")]
    Mobile = 1,

    /// <summary>
    /// Web
    /// </summary>
    [EnumMember(Value = "WEB")]
    Web = 2,
}